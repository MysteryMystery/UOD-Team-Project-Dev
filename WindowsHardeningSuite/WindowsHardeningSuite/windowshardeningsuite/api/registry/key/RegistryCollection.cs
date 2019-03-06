using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                if (!IsSorted())
                    Sort();
                return _regKeys;
            }
            set => _regKeys = value;
        }

        public List<RegistryObject> RegKeysAsList => new List<RegistryObject>(RegKeys);

        public void Add(RegistryObject registryObject)
        {
            List<RegistryObject> lst = new List<RegistryObject>(RegKeys);
            lst.Add(registryObject);
            RegKeys = lst.ToArray();
        }

        private bool IsSorted()
        {
            return false;
        }

        /// <summary>
        /// Sorts the keys
        /// </summary>
        private void Sort()
        {
            var toBe = new LinkedList<RegistryObject>();
            var groups = Group();
            foreach (var key in groups.Keys)
            {
                List<RegistryObject> lst = new List<RegistryObject>(groups[key]);
                lst.Sort((x, y) =>
                {
                    string xFull = x.Location + "\\" + x.Location;
                    string yFull = y.Location + "\\" + y.Location;
                    return String.Compare(xFull, yFull, StringComparison.Ordinal);
                });
                groups[key] = lst;
            }

            foreach (var key in groups.Keys)
            {
                groups[key].ForEach(e => toBe.AddLast(e));   
            }

            RegKeys = toBe.ToArray();
        }

        private Dictionary<String, List<RegistryObject>> Group()
        {
            Dictionary<String, List<RegistryObject>> cats = new Dictionary<string, List<RegistryObject>>();

            foreach (var key in RegKeys)
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
    }
}