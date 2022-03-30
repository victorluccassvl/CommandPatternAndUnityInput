# CommandPatternAndUnityInput
Study project : Command Pattern applied to inputs, and Unity new input system

The idea of this small project is to explore the Command Pattern as well as the modern Unity input system.
The scene consists of 3 small cubes capable of inflating, moving and jumping when using a input layout, as well as deflating rotating and sidesteping when using another one.
In order to select a cube, you must click at it. After that, it will listen inputs with [spacebar], [WASD] and [F].
You can change the input layouts by pressing [E].

In order to justify the use of the Command Pattern, there is an implementation of recording each comand and latter playing that replay.
In order to start or stop recording, you can press [R].
In order to play the record, you can press [P].
