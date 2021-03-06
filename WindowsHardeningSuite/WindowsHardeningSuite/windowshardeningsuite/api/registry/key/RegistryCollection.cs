using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;
using WindowsHardeningSuite.windowshardeningsuite.api.config;
using Microsoft.Win32;

namespace WindowsHardeningSuite.windowshardeningsuite.api.registry.key
{
    /// <summary>
    /// Main resource class for the reg keys
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RegistryCollection : Resource
    {
        private RegistryObject[] _regKeys = new RegistryObject[0];

        [JsonProperty]
        public KeyCategory[] KeyCategories { get; set; } = new KeyCategory[0];
        
        [JsonProperty]
        public RegistryObject[] RegKeys
        {
            get
            {
                if (!IsSorted)
                  Sort();
                return _regKeys;
            }
            set => _regKeys = value;
        }

        public List<RegistryObject> RegKeysAsList => RegKeys.ToList();

        /// <summary>
        /// @Deprecated
        /// Recommended to use this.RegKeysAsList.FindAll();
        /// </summary>
        /// <param name="function">The predicate</param>
        /// <returns>Filtered list</returns>
        [Obsolete]
        public List<RegistryObject> Filter(Predicate<RegistryObject> function)
        {
            return RegKeysAsList.FindAll(function);
        }

        public void Add(RegistryObject registryObject)
        {
            List<RegistryObject> lst = new List<RegistryObject>(RegKeys);
            lst.Add(registryObject);
            RegKeys = lst.ToArray();
        }

        private bool IsSorted { get;  set; } = false;

        private void SortKeys()
        {
            /*List<RegistryObject> registryKeys = RegKeys.ToList();
            registryKeys.Sort((x, y) => {
                return String.Compare(x.DisplayName, y.DisplayName);
            });
            Array.Resize<RegistryObject>(ref _regKeys, registryKeys.Count);
            RegKeys = registryKeys.ToArray();*/
            
            List<RegistryObject> registryKeys = RegKeys.ToList();
            IOrderedEnumerable<RegistryObject> newKeys =
                from registryObject in registryKeys
                where true
                orderby registryObject.Category, registryObject.ID
                select registryObject;
            RegKeys = newKeys.ToArray();

            IsSorted = true;
        }

        /// <summary>
        /// Sorts the keys
        /// </summary>
        private void Sort()
        {
            var toBe = new LinkedList<RegistryObject>();
            Dictionary<String, List<RegistryObject>> groups = Group();
            foreach (var key in groups.Keys)
            {
                List<RegistryObject> lst = groups[key];
                lst.Sort((x, y) =>
                {
                    string xFull = x.DisplayName;
                    string yFull = y.DisplayName;
                    return String.Compare(xFull, yFull, StringComparison.Ordinal);
                });
                groups[key] = lst;
            }

            foreach (var key in groups.Keys)
            {
                groups[key].ForEach(e => toBe.AddLast(e));   
            }

            RegKeys = toBe.ToArray();
            IsSorted = true;
        }

        private Dictionary<String, List<RegistryObject>> Group()
        {
            Dictionary<String, List<RegistryObject>> cats = new Dictionary<string, List<RegistryObject>>();

            foreach (var key in _regKeys)
            {
                if (cats.ContainsKey(key.Category))
                {
                    var l = cats[key.Category];
                    l.Add(key);
                    cats[key.Category] = l;
                }
                else
                {
                    cats[key.Category] = new List<RegistryObject>()
                    {
                        key
                    };
                }
            }

            return cats;
        }

        /// <summary>
        /// Set all the loaded keys to their recommended values.
        /// </summary>
        public void SetAllRecommended()
        {
            RegKeysAsList.ForEach(key => key.SetValue(key.RecommendedValue));
        }

        public void SetAllOff()
        {
            RegKeysAsList.ForEach(key => key.SetValue(key.OffValue));
        }
    }
}