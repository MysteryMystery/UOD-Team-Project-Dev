# Little Brother Privacy Tool

## How the backend is laid out
* Get the list of reg keys using 
```c# 
ResourceProvider.ProvideJSON<RegistryCollection>(Resources.keys) 
```

It is meant to be a singleton.

From there, I recommend linking each entry in the list view to ```RegistryObject``` which would then allow you to reference the objects and enable easy argument passing. Otherwise you need to search for the registry objects, which would be O(n) due to them not being sorted. 

Use ```RegistryCollection::SetAllRecommended()``` for the recommended settings.
To set an individual key to recommended, do ```key.SetValue(key.RecommendedValue)```

## Libraries
GUI Building: https://mahapps.com/
Lite DB: http://www.litedb.org/

## Recommended Application startup flow:

```c#
 DatabaseWrapper wrapper = DatabaseWrapper.GetInstance();
 RegistryCollection registryCollection = ResourceProvider.ProvideJSON<RegistryCollection>(Resources.keys);
 ```
 
## On each key change...
```c#
TrackedChange change = new TrackedChange {
	RegKeyId = key.id;
	SetValue = key.RecommendedValue // OR key.OffValue
	TimeStamp = new Date();
};

wrapper.Insert<TrackedChange>(change);
key.SetValue(key.RecommendedValue);  // OR key.SetValue(key.OffValue);
```