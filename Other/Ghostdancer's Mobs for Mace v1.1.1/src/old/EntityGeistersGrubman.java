// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src;

import java.util.List;
import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            EntityZombie, World, NBTTagCompound, DamageSource, 
//            EntityPlayer, AxisAlignedBB, Entity, Item, 
//            ItemStack

public class EntityGeistersGrubman extends EntityMob
{

    private static final ItemStack defaultHeldItem;

    public EntityGeistersGrubman(World world)
    {
        super(world);
        texture = "/mob/geistersgrubman.png";
    }

    public int getMaxHealth()
    {
        return 20;
    }

    public void writeEntityToNBT(NBTTagCompound nbttagcompound)
    {
        super.writeEntityToNBT(nbttagcompound);
    }

    public void readEntityFromNBT(NBTTagCompound nbttagcompound)
    {
        super.readEntityFromNBT(nbttagcompound);
    }

   protected String getLivingSound()
    {
        return "mob.zombiepig.zpig";
    }

    protected String getHurtSound()
    {
        return "mob.zombiepig.zpighurt";
    }

    protected String getDeathSound()
    {
        return "mob.zombiepig.zpigdeath";
    }

    protected int getDropItemId()
    {
        return Block.mycelium.blockID;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.shovelWood, 1);
    }
}
