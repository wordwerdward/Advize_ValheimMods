﻿using BepInEx.Configuration;
using ServerSync;

namespace Advize_PlantEverything.Configuration
{
    class ModConfig
    {
        private ConfigFile Config;
        private ConfigSync ConfigSync;

        //General 4
        private readonly ConfigEntry<bool> serverConfigLocked;
        private readonly ConfigEntry<int> nexusID; //local
        private readonly ConfigEntry<bool> enableDebugMessages; //local
        private readonly ConfigEntry<bool> alternateIcons;
        private readonly ConfigEntry<bool> alwaysShowSpawners;
        private readonly ConfigEntry<bool> enableMiscFlora;
        private readonly ConfigEntry<bool> enableLocalization; //local
        private readonly ConfigEntry<string> language; //local

        //Difficulty 7
        private readonly ConfigEntry<bool> requireCultivation;
        private readonly ConfigEntry<bool> placeAnywhere;
        private readonly ConfigEntry<bool> enforceBiomes;
        private readonly ConfigEntry<bool> enforceBiomesVanilla;
        private readonly ConfigEntry<bool> enableCropOverrides;
        private readonly ConfigEntry<bool> recoverResources;
        private readonly ConfigEntry<bool> resourcesSpawnEmpty;

        //Berries 9
        private readonly ConfigEntry<int> raspberryCost;
        private readonly ConfigEntry<int> blueberryCost;
        private readonly ConfigEntry<int> cloudberryCost;
        private readonly ConfigEntry<int> raspberryRespawnTime;
        private readonly ConfigEntry<int> blueberryRespawnTime;
        private readonly ConfigEntry<int> cloudberryRespawnTime;
        private readonly ConfigEntry<int> raspberryReturn;
        private readonly ConfigEntry<int> blueberryReturn;
        private readonly ConfigEntry<int> cloudberryReturn;

        //Crops 6
        private readonly ConfigEntry<float> cropMinScale;
        private readonly ConfigEntry<float> cropMaxScale;
        private readonly ConfigEntry<float> cropGrowTimeMin;
        private readonly ConfigEntry<float> cropGrowTimeMax;
        private readonly ConfigEntry<float> cropGrowRadius;
        private readonly ConfigEntry<bool> cropRequireCultivation;

        //Mushrooms 9
        private readonly ConfigEntry<int> mushroomCost;
        private readonly ConfigEntry<int> yellowMushroomCost;
        private readonly ConfigEntry<int> blueMushroomCost;
        private readonly ConfigEntry<int> mushroomRespawnTime;
        private readonly ConfigEntry<int> yellowMushroomRespawnTime;
        private readonly ConfigEntry<int> blueMushroomRespawnTime;
        private readonly ConfigEntry<int> mushroomReturn;
        private readonly ConfigEntry<int> yellowMushroomReturn;
        private readonly ConfigEntry<int> blueMushroomReturn;

        //Flowers 6
        private readonly ConfigEntry<int> thistleCost;
        private readonly ConfigEntry<int> dandelionCost;
        private readonly ConfigEntry<int> thistleRespawnTime;
        private readonly ConfigEntry<int> dandelionRespawnTime;
        private readonly ConfigEntry<int> thistleReturn;
        private readonly ConfigEntry<int> dandelionReturn;

        //Saplings 24
        //private readonly ConfigEntry<int> birchCost;
        //private readonly ConfigEntry<int> oakCost;
        //private readonly ConfigEntry<int> ancientCost;
        private readonly ConfigEntry<float> birchMinScale;
        private readonly ConfigEntry<float> birchMaxScale;
        private readonly ConfigEntry<float> oakMinScale;
        private readonly ConfigEntry<float> oakMaxScale;
        private readonly ConfigEntry<float> ancientMinScale;
        private readonly ConfigEntry<float> ancientMaxScale;
        private readonly ConfigEntry<float> birchGrowthTime;
        private readonly ConfigEntry<float> oakGrowthTime;
        private readonly ConfigEntry<float> ancientGrowthTime;
        private readonly ConfigEntry<float> birchGrowRadius;
        private readonly ConfigEntry<float> oakGrowRadius;
        private readonly ConfigEntry<float> ancientGrowRadius;

        private readonly ConfigEntry<float> beechGrowthTime;
        private readonly ConfigEntry<float> pineGrowthTime;
        private readonly ConfigEntry<float> firGrowthTime;
        private readonly ConfigEntry<float> beechMinScale;
        private readonly ConfigEntry<float> beechMaxScale;
        private readonly ConfigEntry<float> pineMinScale;
        private readonly ConfigEntry<float> pineMaxScale;
        private readonly ConfigEntry<float> firMinScale;
        private readonly ConfigEntry<float> firMaxScale;
        private readonly ConfigEntry<float> beechGrowRadius;
        private readonly ConfigEntry<float> pineGrowRadius;
        private readonly ConfigEntry<float> firGrowRadius;

        //Seeds 4
        private readonly ConfigEntry<int> seedDropMin;
        private readonly ConfigEntry<int> seedDropMax;
        private readonly ConfigEntry<float> dropChance;
        private readonly ConfigEntry<bool> oneOfEach;

        //UI
        private readonly ConfigEntry<bool> enablePickableTimers; //local
        private readonly ConfigEntry<bool> enablePlantTimers; //local
        private readonly ConfigEntry<bool> growthAsPercentage; //local

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
        {
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) => config(group, name, value, new ConfigDescription(description), synchronizedSetting);

        internal ModConfig(ConfigFile configFile, ConfigSync configSync)
        {
            Config = configFile; ConfigSync = configSync;

            //General
            serverConfigLocked = config(
                "General",
                "LockConfiguration",
                false,
                "If on, the configuration is locked and can be changed by server admins only.");
            nexusID = config(
                "General",
                "NexusID",
                1042,
                new ConfigDescription("Nexus mod ID for updates.", null, new ConfigurationManagerAttributes{ Category = "Internal", ReadOnly = true }),
                false);
            enableDebugMessages = config(
                "General",
                "EnableDebugMessages",
                false,
                "Enable mod debug messages in console",
                false);
            alternateIcons = config(
                "General",
                "AlternateIcons",
                false,
                "Use berry icons in the cultivator menu rather than the default ones");
            alwaysShowSpawners = config(
                "General",
                "AlwaysShowSpawners",
                false,
                "Continue to show mushroom, thistle, and dandelion spawners after being picked. Thistle will lose the glow effect until ready to harvest.");
            enableMiscFlora = config(
                "General",
                "EnableMiscFlora",
                true,
                "Enables small trees, bushes, shrubs, and vines.");
            enableLocalization = config(
                "General",
                "EnableLocalization",
                false,
                "Enable this to attempt to load localized text data for the language set in the following setting",
                false);
            language = config(
                "General",
                "Language",
                "english",
                "Language to be used. If EnableLocalization is enabled, game will attempt to load localized text from a file named {language}_PlantEverything.json",
                false);

            //Difficulty
            requireCultivation = config(
                "Difficulty",
                "RequireCultivation",
                false,
                "Pickable resources can only be planted on cultivated ground");
            placeAnywhere = config(
                "Difficulty",
                "PlaceAnywhere",
                false,
                "Allow resources to be placed anywhere (not just on the ground). Does not apply to mushrooms or flowers");
            enforceBiomes = config(
                "Difficulty",
                "EnforceBiomes",
                false,
                "Restrict modded plantables to being placed in their respective biome");
            enforceBiomesVanilla = config(
                "Difficulty",
                "EnforceBiomesVanilla",
                true,
                "Restrict vanilla plantables to being placed in their respective biome");
            enableCropOverrides = config(
                "Difficulty",
                "EnableCropOverrides",
                false,
                "Enables the [Crops] section of this config");
            recoverResources = config(
                "Difficulty",
                "RecoverResources",
                false,
                "Recover resources when pickables are removed with the cultivator. Applies to berries, mushrooms, and flowers");
            resourcesSpawnEmpty = config(
                "Difficulty",
                "ResourcesSpawnEmpty",
                false,
                "Specifies whether resources should spawn empty or full. Currently only applies to berry bushes");

            //Berries
            raspberryCost = config(
                "Berries",
                "RaspberryCost",
                5,
                "Number of raspberries required to place a raspberry bush. Set to 0 to disable the ability to plant this resource");
            blueberryCost = config(
                "Berries",
                "BlueberryCost",
                5,
                "Number of blueberries required to place a blueberry bush. Set to 0 to disable the ability to plant this resource");
            cloudberryCost = config(
                "Berries",
                "CloudberryCost",
                5,
                "Number of cloudberries required to place a cloudberry bush. Set to 0 to disable the ability to plant this resource");
            raspberryRespawnTime = config(
                "Berries",
                "RaspberryRespawnTime",
                300,
                "Number of minutes it takes for a raspberry bush to respawn berries");
            blueberryRespawnTime = config(
                "Berries",
                "BlueberryRespawnTime",
                300,
                "Number of minutes it takes for a blueberry bush to respawn berries");
            cloudberryRespawnTime = config(
                "Berries",
                "CloudberryRespawnTime",
                300,
                "Number of minutes it takes for a cloudberry bush to respawn berries");
            raspberryReturn = config(
                "Berries",
                "RaspberryReturn",
                1,
                "Number of berries a raspberry bush will spawn");
            blueberryReturn = config(
                "Berries",
                "BlueberryReturn",
                1,
                "Number of berries a blueberry bush will spawn");
            cloudberryReturn = config(
                "Berries",
                "CloudberryReturn",
                1,
                "Number of berries a cloudberry bush will spawn");

            //Crops
            cropMinScale = config(
                "Crops",
                "CropMinScale",
                0.9f,
                "The minimum scaling factor used to scale crops upon growth");
            cropMaxScale = config(
                "Crops",
                "CropMaxScale",
                1.1f,
                "The maximum scaling factor used to scale crops upon growth");
            cropGrowTimeMin = config(
                "Crops",
                "CropGrowTimeMin",
                4000f,
                "Minimum number of seconds it takes for crops to grow (will take at least 10 seconds after planting to grow)");
            cropGrowTimeMax = config(
                "Crops",
                "CropGrowTimeMax",
                5000f,
                "Maximum number of seconds it takes for crops to grow (will take at least 10 seconds after planting to grow)");
            cropGrowRadius = config(
                "Crops",
                "CropGrowRadius",
                0.5f,
                "Radius of free space required for crops to grow");
            cropRequireCultivation = config(
                "Crops",
                "CropsRequireCultivation",
                true,
                "Crops can only be planted on cultivated ground");

            //Mushrooms
            mushroomCost = config(
                "Mushrooms",
                "MushroomCost",
                5,
                "Number of mushrooms required to place a pickable mushroom spawner. Set to 0 to disable the ability to plant this resource");
            yellowMushroomCost = config(
                "Mushrooms",
                "YellowMushroomCost",
                5,
                "Number of yellow mushrooms required to place a pickable yellow mushroom spawner. Set to 0 to disable the ability to plant this resource");
            blueMushroomCost = config(
                "Mushrooms",
                "BlueMushroomCost",
                5,
                "Number of blue mushrooms required to place a pickable blue mushroom spawner. Set to 0 to disable the ability to plant this resource");
            mushroomRespawnTime = config(
                "Mushrooms",
                "MushroomRespawnTime",
                240,
                "Number of minutes it takes for mushrooms to respawn");
            yellowMushroomRespawnTime = config(
                "Mushrooms",
                "YellowMushroomRespawnTime",
                240,
                "Number of minutes it takes for yellow mushrooms to respawn");
            blueMushroomRespawnTime = config(
                "Mushrooms",
                "BlueMushroomRespawnTime",
                240,
                "Number of minutes it takes for blue mushrooms to respawn");
            mushroomReturn = config(
                "Mushrooms",
                "MushroomReturn",
                1,
                "Number of mushrooms a pickable mushroom spawner will spawn");
            yellowMushroomReturn = config(
                "Mushrooms",
                "YellowMushroomReturn",
                1,
                "Number of yellow mushrooms a pickable yellow mushroom spawner will spawn");
            blueMushroomReturn = config(
                "Mushrooms",
                "BlueMushroomReturn",
                1,
                "Number of blue mushrooms a pickable blue mushroom spawner will spawn");

            //Flowers
            thistleCost = config(
                "Flowers",
                "ThistleCost",
                5,
                "Number of thistle required to place a pickable thistle spawner. Set to 0 to disable the ability to plant this resource");
            dandelionCost = config(
                "Flowers",
                "DandelionCost",
                5,
                "Number of dandelion required to place a pickable dandelion spawner. Set to 0 to disable the ability to plant this resource");
            thistleRespawnTime = config(
                "Flowers",
                "ThistleRespawnTime",
                240,
                "Number of minutes it takes for thistle to respawn");
            dandelionRespawnTime = config(
                "Flowers",
                "DandelionRespawnTime",
                240,
                "Number of minutes it takes for dandelion to respawn");
            thistleReturn = config(
                "Flowers",
                "ThistleReturn",
                1,
                "Number of thistle a pickable thistle spawner will spawn");
            dandelionReturn = config(
                "Flowers",
                "DandelionReturn",
                1,
                "Number of dandelion a pickable dandelion spawner will spawn");

            //Saplings
            //birchCost = config.Bind(
            //    "Saplings",
            //    "BirchCost",
            //    1,
            //    "Number of birch cones required to place a birch sapling");
            //oakCost = config.Bind(
            //    "Saplings",
            //    "OakCost",
            //    1,
            //    "Number of oak seeds required to place an oak sapling");
            //ancientCost = config.Bind(
            //    "Saplings",
            //    "AncientCost",
            //    1,
            //    "Number of ancient seeds required to place an ancient sapling");
            birchMinScale = config(
                "Saplings",
                "BirchMinScale",
                0.5f,
                "The minimum scaling factor used to scale a birch tree upon growth");
            birchMaxScale = config(
                "Saplings",
                "BirchMaxScale",
                2f,
                "The maximum scaling factor used to scale a birch tree upon growth");
            oakMinScale = config(
                "Saplings",
                "OakMinScale",
                0.5f,
                "The minimum scaling factor used to scale an oak tree upon growth");
            oakMaxScale = config(
                "Saplings",
                "OakMaxScale",
                2f,
                "The maximum scaling factor used to scale an oak tree upon growth");
            ancientMinScale = config(
                "Saplings",
                "AncientMinScale",
                0.5f,
                "The minimum scaling factor used to scale an ancient tree upon growth");
            ancientMaxScale = config(
                "Saplings",
                "AncientMaxScale",
                2f,
                "The maximum scaling factor used to scale an ancient tree upon growth");
            birchGrowthTime = config(
                "Saplings",
                "BirchGrowthTime",
                3000f,
                "Number of seconds it takes for a birch tree to grow from a birch sapling (will take at least 10 seconds after planting to grow)");
            oakGrowthTime = config(
                "Saplings",
                "OakGrowthTime",
                3000f,
                "Number of seconds it takes for an oak tree to grow from an oak sapling (will take at least 10 seconds after planting to grow)");
            ancientGrowthTime = config(
                "Saplings",
                "AncientGrowthTime",
                3000f,
                "Number of seconds it takes for an ancient tree to grow from an ancient sapling (will take at least 10 seconds after planting to grow)");
            birchGrowRadius = config(
                "Saplings",
                "BirchGrowRadius",
                2f,
                "Radius of free space required for a birch sapling to grow");
            oakGrowRadius = config(
                "Saplings",
                "OakGrowRadius",
                2f,
                "Radius of free space required for an oak sapling to grow");
            ancientGrowRadius = config(
                "Saplings",
                "AncientGrowRadius",
                2f,
                "Radius of free space required for an ancient sapling to grow");
            beechGrowthTime = config(
                "Saplings",
                "BeechGrowthTime",
                3000f,
                "Number of seconds it takes for a beech tree to grow from a beech sapling (will take at least 10 seconds after planting to grow)");
            pineGrowthTime = config(
                "Saplings",
                "PineGrowthTime",
                3000f,
                "Number of seconds it takes for a pine tree to grow from a pine sapling (will take at least 10 seconds after planting to grow)");
            firGrowthTime = config(
                "Saplings",
                "FirGrowthTime",
                3000f,
                "Number of seconds it takes for a fir tree to grow from a fir sapling (will take at least 10 seconds after planting to grow)");
            beechMinScale = config(
                "Saplings",
                "BeechMinScale",
                0.5f,
                "The minimum scaling factor used to scale a beech tree upon growth");
            beechMaxScale = config(
                "Saplings",
                "BeechMaxScale",
                2f,
                "The maximum scaling factor used to scale a beech tree upon growth");
            pineMinScale = config(
                "Saplings",
                "PineMinScale",
                0.5f,
                "The minimum scaling factor used to scale a pine tree upon growth");
            pineMaxScale = config(
                "Saplings",
                "PineMaxScale",
                2f,
                "The maximum scaling factor used to scale a pine tree upon growth");
            firMinScale = config(
                "Saplings",
                "FirMinScale",
                0.5f,
                "The minimum scaling factor used to scale a fir tree upon growth");
            firMaxScale = config(
                "Saplings",
                "FirMaxScale",
                2f,
                "The maximum scaling factor used to scale a fir tree upon growth");
            beechGrowRadius = config(
                "Saplings",
                "BeechGrowRadius",
                2f,
                "Radius of free space required for a beech sapling to grow");
            pineGrowRadius = config(
                "Saplings",
                "PineGrowRadius",
                2f,
                "Radius of free space required for a pine sapling to grow");
            firGrowRadius = config(
                "Saplings",
                "FirGrowRadius",
                2f,
                "Radius of free space required for a fir sapling to grow");

            //Seeds
            seedDropMin = config(
                "Seeds",
                "seedDropMin",
                1,
                "Determines minimum amount of seeds that can drop when trees drop seeds");
            seedDropMax = config(
                "Seeds",
                "seedDropMax",
                2,
                "Determines maximum amount of seeds that can drop when trees drop seeds");
            dropChance = config(
                "Seeds",
                "dropChance",
                0.5f,
                "Chance that items will drop from trees when destroyed. Default value 0.5f (50%), will only drop one or two items on loot table unless oneOfEach is set to true. Set between 0 and 1f");
            oneOfEach = config(
                "Seeds",
                "oneOfEach",
                false,
                "When enabled, destroyed trees will drop every available item on their drop table when loot is dropped. Setting this to true will ensure that seeds are guaranteed to drop whenever the tree drops items (governed by dropChance setting)");

            //UI
            enablePickableTimers = config(
                "UI",
                "EnablePickableTimers",
                true,
                "Enables display of growth time remaining on pickable resources.",
                false);
            enablePlantTimers = config(
                "UI",
                "EnablePlantTimers",
                true,
                "Enables display of growth time remaining on planted resources, such as crops and saplings.",
                false);
            growthAsPercentage = config(
                "UI",
                "GrowthAsPercentage",
                false,
                "Enables display of growth time as a percentage instead of time remaining.",
                false);

            //General
            //resourcesSpawnEmpty.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            alternateIcons.SettingChanged += PlantEverything.CoreSettingChanged;
            alwaysShowSpawners.SettingChanged += PlantEverything.CoreSettingChanged;
            enableMiscFlora.SettingChanged += PlantEverything.CoreSettingChanged;

            //Difficulty
            requireCultivation.SettingChanged += PlantEverything.CoreSettingChanged;
            placeAnywhere.SettingChanged += PlantEverything.CoreSettingChanged;
            enforceBiomes.SettingChanged += PlantEverything.CoreSettingChanged;
            //enforceBiomesVanilla.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //enableCropOverrides.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            recoverResources.SettingChanged += PlantEverything.CoreSettingChanged;
            //resourcesSpawnEmpty.SettingChanged += PlantEverything.ConfigurationSettingChanged;

            //Berries
            raspberryCost.SettingChanged += PlantEverything.PickableSettingChanged;
            blueberryCost.SettingChanged += PlantEverything.PickableSettingChanged;
            cloudberryCost.SettingChanged += PlantEverything.PickableSettingChanged;
            raspberryRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            blueberryRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            cloudberryRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            raspberryReturn.SettingChanged += PlantEverything.PickableSettingChanged;
            blueberryReturn.SettingChanged += PlantEverything.PickableSettingChanged;
            cloudberryReturn.SettingChanged += PlantEverything.PickableSettingChanged;

            //Crops
            //cropMinScale.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //cropMaxScale.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //cropGrowTimeMin.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //cropGrowTimeMax.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //cropGrowRadius.SettingChanged += PlantEverything.ConfigurationSettingChanged;
            //cropRequireCultivation.SettingChanged += PlantEverything.ConfigurationSettingChanged;

            //Mushrooms
            mushroomCost.SettingChanged += PlantEverything.PickableSettingChanged;
            yellowMushroomCost.SettingChanged += PlantEverything.PickableSettingChanged;
            blueMushroomCost.SettingChanged += PlantEverything.PickableSettingChanged;
            mushroomRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            yellowMushroomRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            blueMushroomRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            mushroomReturn.SettingChanged += PlantEverything.PickableSettingChanged;
            yellowMushroomReturn.SettingChanged += PlantEverything.PickableSettingChanged;
            blueMushroomReturn.SettingChanged += PlantEverything.PickableSettingChanged;

            //Flowers
            thistleCost.SettingChanged += PlantEverything.PickableSettingChanged;
            dandelionCost.SettingChanged += PlantEverything.PickableSettingChanged;
            thistleRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            dandelionRespawnTime.SettingChanged += PlantEverything.PickableSettingChanged;
            thistleReturn.SettingChanged += PlantEverything.PickableSettingChanged;
            dandelionReturn.SettingChanged += PlantEverything.PickableSettingChanged;

            //Saplings
            ancientGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            ancientGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            ancientMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            ancientMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            beechGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            beechGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            beechMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            beechMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            pineGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            pineGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            pineMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            pineMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            firGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            firGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            firMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            firMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            birchGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            birchGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            birchMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            birchMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            oakGrowRadius.SettingChanged += PlantEverything.SaplingSettingChanged;
            oakGrowthTime.SettingChanged += PlantEverything.SaplingSettingChanged;
            oakMinScale.SettingChanged += PlantEverything.SaplingSettingChanged;
            oakMaxScale.SettingChanged += PlantEverything.SaplingSettingChanged;

            //Seeds
            seedDropMin.SettingChanged += PlantEverything.SeedSettingChanged;
            seedDropMax.SettingChanged += PlantEverything.SeedSettingChanged;
            dropChance.SettingChanged += PlantEverything.SeedSettingChanged;
            oneOfEach.SettingChanged += PlantEverything.SeedSettingChanged;

            configSync.AddLockingConfigEntry(serverConfigLocked);

            //Config.SettingChanged += PlantEverything.ConfigSettingChanged;
        }

        internal bool EnableDebugMessages
        {
            get { return enableDebugMessages.Value; }
        }
        internal bool AlternateIcons
        {
            get { return alternateIcons.Value; }
        }
        internal bool AlwaysShowSpawners
        {
            get { return alwaysShowSpawners.Value; }
        }
        internal bool EnableMiscFlora
        {
            get { return enableMiscFlora.Value; }
        }
        internal bool EnableLocalization
        {
            get { return enableLocalization.Value; }
        }
        internal string Language
        {
            get { return language.Value; }
        }
        internal bool RequireCultivation
        {
            get { return requireCultivation.Value; }
        }
        internal bool PlaceAnywhere
        {
            get { return placeAnywhere.Value; }
        }
        internal bool EnforceBiomes
        {
            get { return enforceBiomes.Value; }
        }
        internal bool EnforceBiomesVanilla
        {
            get { return enforceBiomesVanilla.Value; }
        }
        internal bool EnableCropOverrides
        {
            get { return enableCropOverrides.Value; }
        }
        internal bool RecoverResources
        {
            get { return recoverResources.Value; }
        }
        internal bool ResourcesSpawnEmpty
        {
            get { return resourcesSpawnEmpty.Value; }
        }
        internal int RaspberryCost
        {
            get { return raspberryCost.Value; }
        }
        internal int BlueberryCost
        {
            get { return blueberryCost.Value; }
        }
        internal int CloudberryCost
        {
            get { return cloudberryCost.Value; }
        }
        internal int RaspberryRespawnTime
        {
            get { return raspberryRespawnTime.Value; }
        }
        internal int BlueberryRespawnTime
        {
            get { return blueberryRespawnTime.Value; }
        }
        internal int CloudberryRespawnTime
        {
            get { return cloudberryRespawnTime.Value; }
        }
        internal int RaspberryReturn
        {
            get { return raspberryReturn.Value; }
        }
        internal int BlueberryReturn
        {
            get { return blueberryReturn.Value; }
        }
        internal int CloudberryReturn
        {
            get { return cloudberryReturn.Value; }
        }
        internal float CropMinScale
        {
            get { return cropMinScale.Value; }
        }
        internal float CropMaxScale
        {
            get { return cropMaxScale.Value; }
        }
        internal float CropGrowTimeMin
        {
            get { return cropGrowTimeMin.Value; }
        }
        internal float CropGrowTimeMax
        {
            get { return cropGrowTimeMax.Value; }
        }
        internal float CropGrowRadius
        {
            get { return cropGrowRadius.Value; }
        }
        internal bool CropRequireCultivation
        {
            get { return cropRequireCultivation.Value; }
        }
        internal int MushroomCost
        {
            get { return mushroomCost.Value; }
        }
        internal int YellowMushroomCost
        {
            get { return yellowMushroomCost.Value; }
        }
        internal int BlueMushroomCost
        {
            get { return blueMushroomCost.Value; }
        }
        internal int MushroomRespawnTime
        {
            get { return mushroomRespawnTime.Value; }
        }
        internal int YellowMushroomRespawnTime
        {
            get { return yellowMushroomRespawnTime.Value; }
        }
        internal int BlueMushroomRespawnTime
        {
            get { return blueMushroomRespawnTime.Value; }
        }
        internal int MushroomReturn
        {
            get { return mushroomReturn.Value; }
        }
        internal int YellowMushroomReturn
        {
            get { return yellowMushroomReturn.Value; }
        }
        internal int BlueMushroomReturn
        {
            get { return blueMushroomReturn.Value; }
        }
        internal int ThistleCost
        {
            get { return thistleCost.Value; }
        }
        internal int DandelionCost
        {
            get { return dandelionCost.Value; }
        }
        internal int ThistleRespawnTime
        {
            get { return thistleRespawnTime.Value; }
        }
        internal int DandelionRespawnTime
        {
            get { return dandelionRespawnTime.Value; }
        }
        internal int ThistleReturn
        {
            get { return thistleReturn.Value; }
        }
        internal int DandelionReturn
        {
            get { return dandelionReturn.Value; }
        }
        //internal int BirchCost
        //{
        //    get { return birchCost.Value; }
        //}
        //internal int OakCost
        //{
        //    get { return oakCost.Value; }
        //}
        //internal int AncientCost
        //{
        //    get { return ancientCost.Value; }
        //}
        internal float BirchMinScale
        {
            get { return birchMinScale.Value; }
        }
        internal float BirchMaxScale
        {
            get { return birchMaxScale.Value; }
        }
        internal float OakMinScale
        {
            get { return oakMinScale.Value; }
        }
        internal float OakMaxScale
        {
            get { return oakMaxScale.Value; }
        }
        internal float AncientMinScale
        {
            get { return ancientMinScale.Value; }
        }
        internal float AncientMaxScale
        {
            get { return ancientMaxScale.Value; }
        }
        internal float BirchGrowthTime
        {
            get { return birchGrowthTime.Value; }
        }
        internal float OakGrowthTime
        {
            get { return oakGrowthTime.Value; }
        }
        internal float AncientGrowthTime
        {
            get { return ancientGrowthTime.Value; }
        }
        internal float BirchGrowRadius
        {
            get { return birchGrowRadius.Value; }
        }
        internal float OakGrowRadius
        {
            get { return oakGrowRadius.Value; }
        }
        internal float AncientGrowRadius
        {
            get { return ancientGrowRadius.Value; }
        }
        internal float BeechGrowthTime
        {
            get { return beechGrowthTime.Value; }
        }
        internal float PineGrowthTime
        {
            get { return pineGrowthTime.Value; }
        }
        internal float FirGrowthTime
        {
            get { return firGrowthTime.Value; }
        }
        internal float BeechMinScale
        {
            get { return beechMinScale.Value; }
        }
        internal float BeechMaxScale
        {
            get { return beechMaxScale.Value; }
        }
        internal float PineMinScale
        {
            get { return pineMinScale.Value; }
        }
        internal float PineMaxScale
        {
            get { return pineMaxScale.Value; }
        }
        internal float FirMinScale
        {
            get { return firMinScale.Value; }
        }
        internal float FirMaxScale
        {
            get { return firMaxScale.Value; }
        }
        internal float BeechGrowRadius
        {
            get { return beechGrowRadius.Value; }
        }
        internal float PineGrowRadius
        {
            get { return pineGrowRadius.Value; }
        }
        internal float FirGrowRadius
        {
            get { return firGrowRadius.Value; }
        }
        internal int SeedDropMin
        {
            get { return seedDropMin.Value; }
        }
        internal int SeedDropMax
        {
            get { return seedDropMax.Value; }
        }
        internal float DropChance
        {
            get { return dropChance.Value; }
        }
        internal bool OneOfEach
        {
            get { return oneOfEach.Value; }
        }
        internal bool EnablePickableTimers
        {
            get { return enablePickableTimers.Value; }
        }
        internal bool EnablePlantTimers
        {
            get { return enablePlantTimers.Value; }
        }
        internal bool GrowthAsPercentage
        {
            get { return growthAsPercentage.Value; }
        }
    }
}
