# IndustrialRobotProject

CURRENT STATE: in progress

Issues:
- !MVVM
- code behind, tight coupling and many others
- depending on Caliburn.Micro, but it is only used for Bootstrapper.cs and empty ViewModels (no functionalities used)

Min requirements:
- parametrizing serial port ✓
- sending and receiving data through serial port (to/from robot) ✓
- sending manual-typed commands ✓
- JOINT jog operation to control robot movement in configuration space ✓

Extra requirements:
- downloading position list ✓
- XYZ Jog operation to control robot movement in Cartesian space ✓
- extra ideas

ToDo:
- MVVM refactoring |in progress|
- graphical improvements (robot image) ✓
- "alphabet printer" (specifying commands for each letter, so the robot could draw it)
