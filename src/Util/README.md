# Basic Utilities

This project contains some basic utilities to allow for the implementation of some patterns that I find helpful.

## Option Pattern

The option pattern is a concept that is borrowed (in a very rudimentary fashion) from functional progamming. An Option can have a value or it can have no value but it is never `null`, the value can be null though!

```c#
Option<string> x = null; // implicit cast to None.
x.HasValue == false
x.Value // throws

x = "Hi";
x.HasValue == true;
x.Value == "Hi"
```

## Object Extension(s)

This is currently only the "ThrowIfNull" implementation to remove null warnings and throw when something is null that should not be null.

Usage:
```c#
string x = null;
var z = x.ThrowIfNull()
// result = ArgumentNullException

x = "Hi";
z = x.ThrowIfNull();
// result = z = "Hi". 
```