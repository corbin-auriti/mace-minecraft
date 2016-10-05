// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            EntityMob, World, MathHelper, Item, 
//            EnumCreatureAttribute

public class EntityGeistersGrim extends EntityZombie
{

    private static final ItemStack defaultHeldItem;

    public EntityGeistersGrim(World world)
    {
        super(world);
        texture = "/mob/geistersgeist.png";
    }

    public int getMaxHealth()
    {
        return 40;
    }

    public void writeEntityToNBT(NBTTagCompound nbttagcompound)
    {
        super.writeEntityToNBT(nbttagcompound);
    }

    public void readEntityFromNBT(NBTTagCompound nbttagcompound)
    {
        super.readEntityFromNBT(nbttagcompound);
    }

    public void setEntityDead()
    {
        if(!worldObj.multiplayerWorld && health == 0)
        {
            for(int j = 0; j < 4; j++)
            {
                float f = ((float)(j % 2) - 0.5F) / 4F;
                float f1 = ((float)(j / 2) - 0.5F) / 4F;
                EntityGeistersGeist entitygeistersgeist = new EntityGeistersGeist(worldObj);
                entitygeistersgeist.setLocationAndAngles(posX + (double)f, posY + 0.5D, posZ + (double)f1, rand.nextFloat() * 360F, 0.0F);
                worldObj.entityJoinedWorld(entitygeistersgeist);
            }
        }
        super.setEntityDead();
    }

    protected String getLivingSound()
    {
        return "mobsformace.geist";
    }

    protected String getHurtSound()
    {
        return "mobsformace.geisthurt";
    }

    protected String getDeathSound()
    {
        return "mobsformace.geistdeath";
    }

    protected int getDropItemId()
    {
        return Item.bone.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.hoeSteel, 1);
    }

    public EnumCreatureAttribute func_40124_t()
    {
        return EnumCreatureAttribute.UNDEAD;
    }
}
