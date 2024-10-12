# PlayerBenchmarker
Tool for CM 01/02 for testing player attributes via benchmarking.

# Installation
1. Either download prebuilt binary from "bin" folder or build it from source via Visual Studio.
2. Copy files from "CM0102 Tactics Benchmarker v1.1" to your CM folder and prefer all necessary preparations. Refer to readme of that tool: https://champman0102.net/viewtopic.php?t=3625
3. Disable Save Compression in game settings. Backup your "game.cfg" file.
4. To make testing quicker it's better to keep a single .sav file in your CM folder.

# System requirements
The faster your CPU the faster you will get your tests done. Ryzen 9 makes 100 runs test in 1-2 minutes.

# Recommendations
1. Apply most critical patches to cm0102_bm1.1.exe via Nick's patcher (fix of creativity, positioning and marking bugs).

# Usage
1. Run the PlayerBenchmarker.exe.
2. Click "Load CM save file". If there is only 1 .sav file in CM folder, program loads that. Otherwise it aks user to select a file.
3. Modifiy attributes as you wish and don't forget to click "Save CM save file" afterwards.
4. Enter unique test name for each test.
5. Enter runs count (amount of seasons to play; bigger the value, the higher is precision, but takes longer time).
6. Enter amount of concurrent runs (as a rule of thumb: amount of cores of your CPU is a good choice).
7. Modify club name if neccessary (depends on your CM save file).
8. Run tests.
9. See results appended to "repository.csv" file under "Bench" folder. Format is: "Name For Ag Pts":
Name - test name;
For - average amount of goals scored by team per season;
Ag - average amount of goals conceded by team per season;
Pts - average amount of points earned by team per season.
Also the original report ('benchresult.csv' file) of "CM0102 Tactics Benchmarker v1.1" is copied to "Bench" folder under name equal to test name.

# Known cm0102_bm1.1.exe issues:
1. Sometimes cm0102_bm1.1.exe crushes. Testing stucks until crushed cm0102_bm1.1.exe instances are closed manually.
2. Sometimes game.cfg becomes corrupted. Better make a backup copy of it for quick replacement.

# How it works
Program splits all players into categories (according to their original positions) and edits position values for each player accordingly:
- GK
- DC (sets Defender = Center = 20, everything else to 1)
- DLR (sets Defender = DefensiveMidfielder = Midfielder = AttackingMidfielder = WingBack = LeftSide = RightSide = 20, everything else to 1)
- DMC (sets DefensiveMidfielder = Midfielder = Center = 20, everything else to 1)
- AMC (sets AttackingMidfielder = Midfielder = 20, everything else to 1)
- FC (sets Attacker = Center = 20, everything else to 1)
If you wish another categories and/or values, the only way ATM is to modify the program code.

# Special thanks
1) Vladimir Filatov, the author of "CM Scout Intrinsic" from which .sav file parsing code was taken.
2) Nick+Co, the author of "Nick's CM0102Patcher" for an awesome tool with rich functionality.
3) Tapani and Xeno, authors of "CM0102 Tactics Benchmarker v1.1" for the bechmarker itself.
4) champman0102.net's community for an awesome resource and keeping the game alive.