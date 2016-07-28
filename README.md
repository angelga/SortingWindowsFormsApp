C# app for running sorting algorithms
===========================================================

I needed to brush up on C# syntax and wrote this little app to run sorting algorithms. I could have written this more beautifully, but the desktop is dead, and web is the future. 

![Screenshot](https://cloud.githubusercontent.com/assets/4750097/17042325/8c8b6e72-4f61-11e6-8398-145e3be85a6b.PNG "Screenshot")

Features
--------
* Provide your own input, or create random input
* Choose from a variety of sorting algorithms
* Logging box with total running time
* Compare result with that of List.Sort() for correctness

Notes
----------

Sorting tasks are performed in a background thread to prevent freezing the UI. This app uses [BackgroundWorker](https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(v=vs.110).aspx) to achieve that.

The app however will freeze when the input or output textbox are updated with large amount of data. To get around that, one could write a background thread to:

* Break the output into chunks
* Update the textbox with AppendText
* Calling SuspendLayout/ResumeLayout before updating textbox

Since this was also not needed for this demo app, a quick workaround is to set the textbox's WordWrap to False, this will greatly improve performance.

Final thoughts
==============

Finally, I also suggest being mindful of people using keyboard shortcuts. So set UI elements's TabIndex appropriately, specially textboxes and buttons, to minimize mouse usage as much as possible. 
