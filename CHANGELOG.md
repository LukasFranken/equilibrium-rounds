### v0.1.0 Sacrifice (latest)
* Sacrifice
	* Block to lose 50% of your current HP. Each point of HP lost increases the next shots damage and speed. It also ignores walls.
* Small code optimizations
* Removed unnessecary dependencies

### v0.0.8 Protos
* Protos (NEW CARD)
	* Gain 30% of the damage you take back as max health (across rounds, like metamorphosis)
* Evasion
	* Card Color: DefensiveBlue -> TechWhite
* Tag
	* Card Color: EvilPurple -> MagicPink
* Dependencies adjusted (more patches to improve performance)

### v0.0.7 Tag rework
* Tag
	* complete code refactor
	* tag lasts for 7 seconds -> tag resets on map change
	* fixed multiple tag bug -> now a new tag replaces the old one
	* fixed a bug where teleporting 2x in a row set the block cooldown permanently to 0

### v0.0.6 Small reset failure fix
* Metamorphosis
	* fixed effect transferring over across rematches (yes you weren't supposed to stack THAT hard)
* Minigun
	* fixed effect transferring over across rematches

### v0.0.5 Adjustments and dependency update
* Evasion
	* changed description "Jump backwards and heal a bit when you block" -> "Jump back and heal on block"
* Cannonball
	* removed the recoil line from the card for space
* Tag
	* made bullet red
	* tag lasts for: 5s -> 7s
	* cooldown between tag and teleport: 50% -> 30% of current cooldown
* added nesscary dependency of CardChoiceSpawnUniqueCardPatch (oops)
* updated changelog and readme to proper markup design

### v0.0.4 Changelog added
* added changelog

### v0.0.3 Added Tag Card
* Tag Card and functionality added
* description Adjustment for Metamorphosis
* small cannon balance
	* damage: +250% -> 300%
	* recoil: +600 -> +700
	* reload time: -50% -> -60%
* small minigun balance
	* damage: -80% -> -70%
	* projectile speed: +100% -> +0% (removed)

### v0.0.2 Tiny Info Update
* added Readme
* updated description in manifest.json

### v0.0.1 Initial Commit
* Added 4 Cards
	* Cannonball
	* Minigun
	* Metamorphosis
	* Evasion