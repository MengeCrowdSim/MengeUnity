# Menge Unity

This project serves as an example for integrating the Menge crowd simulation framework with the
Unity game engine.

## Running the Demo

Make sure you have already cloned the [main Menge repository](https://github.com/MengeCrowdSim/Menge).
This Unity project will reference simulation specifications in the examples directory.

These instructions assume the 64-bit version of the Unity Editor.  If you're using the 32-bit
version, read the instructions below on 32/64-bit issues.

1. Clone this repository (or better yet, fork it so you can submit improvements back as a pull
request).
2. Start the Unity Editor.
3. Open a new project and select the root directory of your clone as the containing folder.
4. The Unity 3D view should show four blocks.  These are the obstacles for the 4square example in
`$MengeRoot$\examples\core\4square`.
![Unity File Open](https://github.com/MengeCrowdSim/MengeUnity/blob/master/doc/images/4_blocks.PNG)
5. Open the `SimController` script for editing.
6. Edit line 27 so the `mengeRoot` variable points to the path on your system where the Menge
source is located.
7. Save `SimController.cs` and return to the editor.
8. Hit the `play` button.
![Unity File Open](https://github.com/MengeCrowdSim/MengeUnity/blob/master/doc/images/4_blocks_sim.PNG)

## Dependencies for Making Changes

The contents of this repository are sufficiently self-contained to run a demo (assuming you can
provide scenario specification files).  However, it may be necessary for you make modifications on
what this simple example can do; changes that go beyond just Unity.  The integration of Menge into
Unity is based on two things:

1. Menge itself (specifically, the `MengeCore.dll`). [Clone from here](https://github.com/MengeCrowdSim/Menge)
2. Menge C-Sharp Wrapper. [Clone from here](https://github.com/MengeCrowdSim/MengeCS)

Both of these projects will produce dlls for you to include in the Unity project.  The `MengeCore.dll`
produced by the Menge project should go in `$MengeUnity$\assets\Plugins` (see note below for details
on 32/64-bit issues). The `MengeCS.dll` produced by the second project should go in the
`$MengeUnity$\assets\Scripts` folder. 

## 32/64-bit issues

When using external dlls in Unity, it is important to make sure the dlls are built to the same 
build environment as the application.  For a 32-bit application, you need 32-bit dlls. For a 64-bit
application, you need a 64-bit dll.  This _seems_ straightforward, but there is a wrinkle that might
catch you.  The Unity Editor is one of the applications you develop and, on modern machines, it is
most likely a 64-bit application.  So, even if your final application you're using Unity to produce
is a 32-bit application, to develop it you'll need both a 64-bit and 32-bit version of the
`MengeCore.dll`.

If you look in the `Plugins` folder, you'll note there are _two_ subfolders containing, apparently
identical files: `x86` and `x86_64` both contain copies of `MengeCore.dll`.  As the directory names
suggeset, they are not actually identical.  The former is a 32-bit dll and the latter is a 64-bit
dll.  We provide both and then configure them _inside_ Unity so that it uses the right one in the
right context.  For this to work, they _must_ be named the same.

If you make changes to Menge, you'll need to build Menge twice: one as 32-bit and one as 64-bit.  
In order for the magic to work, the files must have identical names.  This can lead to confusion and
requires care that the right build ends up in the right directory.  If you end up putting the wrong
dll in the wrong directory, it will become immediately apparent -- the dll will fail to load.

## Contributing

Please feel free to contribute to this example.  The first major step is to replace cylinders with
interesting pedestrians.  We would really appreciate it if those better versed in Unity than we are
could help us flesh this out into a more fully-featured visualization of Menge simulations.