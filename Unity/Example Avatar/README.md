# OWOVRC Example avatar

This package contains an example avatar to showcase the features of [OWOVRC](https://github.com/sync1211/owoskin-vrc).


## Setup Requirements
* [OWOVRC](https://github.com/sync1211/owoskin-vrc)
* [VRCFury](https://vrcfury.com/)
* OWO Skin Avatar prefab (Either from [OWOVRC](https://github.com/sync1211/owoskin-vrc) or [Shadoki](https://github.com/shadorki/vrc-owo-suit))

## Setting up custom sensation presets

1. Download [OWO Sensations Creator](https://owo-game.gitbook.io/owo-api/tools/sensations-creator)
2. Create your sensation and export it as a .owo file
3. Import it into OWOVRC by switching to "Presets" and clicking "Configure"
4. Create a parameter as shown on the example avatar

There are 4 example sensations included in this package.

## Setting up avatar parameters for presets

### Triggering presets
To call a preset, create a boolean parameter named `OWO/SensationsTrigger/<YOUR PRESET NAME>` and set it to true via a menu or [VRC Parameter Driver](https://creators.vrchat.com/avatars/state-behaviors/#avatar-parameter-driver).
To control the intensity of the sensation preset, create a float parameter instead and set its value to the desired intensity. (1 = 100%)

### Triggering loops
If a preset is configured as a loop, it will play until its parameter is set to '0' or 'false'

### Triggering presets on specific muscles
To apply a preset to a specific set of muscles, append the muscles/muscle group to the name of the preset.
For example: `OWO/SensationsTrigger/<YOUR PRESET NAME>/arm_r` will apply the senstion to the right arm only.

Please note that the sensation needs to be exported with "Export with muscles" set to OFF to be able to control what muscles it runs on!
As a result, if no muscle is specified (i.e. if it's triggered via `OWO/SensationsTrigger/<YOUR PRESET NAME>`) the sensation will run on all muscles!

The following muscles and groups are supported (case-sensitive):

Muscles:
* pectoral_r
* pectoral_l
* abdominal_r
* abdominal_l
* arm_r
* arm_l
* dorsal_r
* dorsal_l
* lumbar_r
* lumbar_l

Groups:
* all
* upperChest
* frontMuscles
* backMuscles
* leftMuscles
* rightMuscles

## Disclaimer

This is a community project; I am not affiliated with OWOGame.