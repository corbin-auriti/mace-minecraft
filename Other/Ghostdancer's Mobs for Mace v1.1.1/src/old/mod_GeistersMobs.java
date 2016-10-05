package net.minecraft.src;
import java.io.*;
import java.util.*;
import java.util.Map;
import java.util.Random;
import net.minecraft.client.Minecraft;

public class mod_GeistersMobs extends BaseMod
{

    public static int SpawnChancePoor;
    public static int SpawnChanceRich;
    public static int SpawnChanceSage;
    public static int SpawnChanceMaid;
    public static int SpawnChanceSmith;
    public static int SpawnChanceKnight;
//    public static int SpawnChanceHalfman;
    public static int SpawnChanceRogue;
    public static int SpawnChanceCultist;
    public static int SpawnChanceGeist;
    public static int SpawnChanceGrim;
    public static int SpawnChanceGrubman;
    public static int defaultSpawnPoor = 10;
    public static int defaultSpawnRich = 5;
    public static int defaultSpawnSage = 5;
    public static int defaultSpawnMaid = 10;
    public static int defaultSpawnSmith = 10;
    public static int defaultSpawnKnight = 10;
//    public static int defaultSpawnHalfman = 1;
    public static int defaultSpawnRogue = 10;
    public static int defaultSpawnCultist = 0;
    public static int defaultSpawnGeist = 5;
    public static int defaultSpawnGrim = 1;
    public static int defaultSpawnGrubman = 1;

    public mod_GeistersMobs()
    {
        GeistersMobsProperties geistersmobsproperties = new GeistersMobsProperties();
        try
        {
            File file = new File((new StringBuilder()).append(Minecraft.getMinecraftDir()).append("/GeistersMobs.properties").toString());
            boolean flag = file.createNewFile();
            if(flag)
            {
                FileOutputStream fileoutputstream = new FileOutputStream(file);
                geistersmobsproperties.setProperty("SpawnChancePoor", Integer.toString(defaultSpawnPoor));
                geistersmobsproperties.setProperty("SpawnChanceRich", Integer.toString(defaultSpawnRich));
                geistersmobsproperties.setProperty("SpawnChanceSage", Integer.toString(defaultSpawnSage));
                geistersmobsproperties.setProperty("SpawnChanceMaid", Integer.toString(defaultSpawnMaid));
                geistersmobsproperties.setProperty("SpawnChanceSmith", Integer.toString(defaultSpawnSmith));
                geistersmobsproperties.setProperty("SpawnChanceKnight", Integer.toString(defaultSpawnKnight));
//                geistersmobsproperties.setProperty("SpawnChanceHalfman", Integer.toString(defaultSpawnHalfman));
                geistersmobsproperties.setProperty("SpawnChanceRogue", Integer.toString(defaultSpawnRogue));
                geistersmobsproperties.setProperty("SpawnChanceCultist", Integer.toString(defaultSpawnCultist));
                geistersmobsproperties.setProperty("SpawnChanceGeist", Integer.toString(defaultSpawnGeist));
                geistersmobsproperties.setProperty("SpawnChanceGrim", Integer.toString(defaultSpawnGrim));
                geistersmobsproperties.setProperty("SpawnChanceGrubman", Integer.toString(defaultSpawnGrubman));
                geistersmobsproperties.store(fileoutputstream, "Change the following values to edit the in-game spawn chance:");
                fileoutputstream.close();
            }
            geistersmobsproperties.load(new FileInputStream((new StringBuilder()).append(Minecraft.getMinecraftDir()).append("/GeistersMobs.properties").toString()));
            SpawnChancePoor = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChancePoor"));
            SpawnChanceRich = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceRich"));
            SpawnChanceSage = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceSage"));
            SpawnChanceMaid = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceMaid"));
            SpawnChanceSmith = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceSmith"));
            SpawnChanceKnight = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceKnight"));
//            SpawnChanceHalfman = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceHalfman"));
            SpawnChanceRogue = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceRogue"));
            SpawnChanceCultist = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceCultist"));
            SpawnChanceGeist = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceGeist"));
            SpawnChanceGrim = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceGrim"));
            SpawnChanceGrubman = Integer.parseInt(geistersmobsproperties.getProperty("SpawnChanceGrubman"));
        }
        catch(IOException ioexception)
        {
            ioexception.printStackTrace();
        }

        ModLoader.RegisterEntityID(EntityGeistersPoor.class, "GPoor", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersPoor.class, SpawnChancePoor, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersRich.class, "GRich", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersRich.class, SpawnChanceRich, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersSage.class, "GSage", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersSage.class, SpawnChanceSage, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersMaid.class, "GMaid", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersMaid.class, SpawnChanceMaid, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersSmith.class, "GSmith", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersSmith.class, SpawnChanceSmith, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersKnight.class, "GKnight", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersKnight.class, SpawnChanceKnight, 4, 4, EnumCreatureType.creature);
//        ModLoader.RegisterEntityID(EntityGeistersHalfman.class, "GHalfman", ModLoader.getUniqueEntityId());
//        ModLoader.AddSpawn(EntityGeistersHalfman.class, SpawnChanceHalfman, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersRogue.class, "GRogue", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersRogue.class, SpawnChanceRogue, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersCultist.class, "GCultist", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersCultist.class, SpawnChanceCultist, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersGeist.class, "GGeist", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersGeist.class, SpawnChanceGeist, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersGrim.class, "GGrim", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersGrim.class, SpawnChanceGrim, 4, 4, EnumCreatureType.creature);
        ModLoader.RegisterEntityID(EntityGeistersGrubman.class, "GGrubman", ModLoader.getUniqueEntityId());
        ModLoader.AddSpawn(EntityGeistersGrubman.class, SpawnChanceGrubman, 4, 4, EnumCreatureType.creature);
    }

    public void AddRenderer(Map map)
    {
    	map.put(net.minecraft.src.EntityGeistersPoor.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersRich.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersSage.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersMaid.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersSmith.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersKnight.class, new RenderBiped(new ModelBiped(), 0.5F));
//    	map.put(net.minecraft.src.EntityGeistersHalfman.class, new RenderGeistersHalfman(new ModelGeistersHalfman(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersRogue.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersCultist.class, new RenderBiped(new ModelBiped(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersGeist.class, new RenderBiped(new ModelZombie(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersGrim.class, new RenderBiped(new ModelZombie(), 0.5F));
    	map.put(net.minecraft.src.EntityGeistersGrubman.class, new RenderGeistersGrubman(new ModelGeistersGrubman(), 0.5F));
    }

    public String getVersion()
    {
        return "0.90";
    }

    public void load()
    {
    }

}