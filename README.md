C# app for running sorting algorithms
===========================================================

I needed to brush up on C# syntax and wrote this little app to run sorting algorithms. I could have written this more beautifully, but the desktop is dead, and web is the future. 

Features
--------
* Provide your own input, or create random input
* Choose from a variety of sorting algorithms
* Logging box with total running time
* Compare result with that of List.Sort() for correctness

Challenges
----------

Sorting tasks are performed in a background thread to prevent freezing the UI. This app uses [BackgroundWorker](https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(v=vs.110).aspx) to achieve that.

However, the UI will still freeze. If the app does a background intensive operation, but returns little data, the app does not freeze as expected. However, if the background worker does little work but returns a large amount of data, like a big string, or a long list, the hang will occur only during the marshalling of the data between threads when the background worker completes, not while the worker is computing the sort.

This was unexpected and makes the app look unresponsive. I did lots of researching on this issue, but didn't find any workarounds. If you have some feedback, please contact me.
