// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode fieldsfirst 

package net.minecraft.src;

import java.util.Random;

// Referenced classes of package net.minecraft.src:
//            EntityMob, World, MathHelper, Item, 
//            EnumCreatureAttribute

public class EntityGeistersGeist extends EntityZombie
{

    public EntityGeistersGeist(World world)
    {
        super(world);
        texture = "/mob/geistersgeist.png";
        moveSpeed = 0.5F;
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

    public void onLivingUpdate()
    {
        if(worldObj.isDaytime() && !worldObj.multiplayerWorld)
        {
            float f = getEntityBrightness(1.0F);
            if(f > 0.5F && worldObj.canBlockSeeTheSky(MathHelper.floor_double(posX), MathHelper.floor_double(posY), MathHelper.floor_double(posZ)) && rand.nextFloat() * 30F < (f - 0.4F) * 2.0F)
            {
                func_40046_d(8);
            }
        }
        super.onLivingUpdate();
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
        return Item.slimeBall.shiftedIndex;
    }

    public EnumCreatureAttribute func_40124_t()
    {
        return EnumCreatureAttribute.UNDEAD;
    }
}
