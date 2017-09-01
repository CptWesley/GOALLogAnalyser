## GOALLogAnalyser
Analyses GOAL logs.

### Current Features:
#### Per Agent:
- Module statistics
- Cycle statistics
#### Per Agent Type:
- Module statistics
- Cycle statistics
#### Output formats:
- Text files
- JSON
- Website (provided by @pvdstel at https://github.com/pvdstel/log-tablr)


### How to use:
#### Preperation:
1. In _Eclipse_ > __Window__ > __Preferences__
2. _Goal_ > __Logging__
3. Enable:
    * Write Logs to file
    * The reasoning seperator
    * Include statistics each cycle seperator
    * Entry of a module
    * Exit of a module
    * Evaluation of rule conditions
4. Run the MAS.
#### Analyse:
0. __*NB: By default this program will export all formats. Start using the `-json`, `-text`, `-site` flag arguments to get single type of output.*__
1. Drag and drop all log files onto the executable to process the data.
2. Wait until the program finishes.
3. Output can be found in the newly created __*output*__ directory located in the current working directory.
