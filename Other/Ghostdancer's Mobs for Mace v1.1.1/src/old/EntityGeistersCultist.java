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

public class EntityGeistersCultist extends EntityMob
{

    private static final ItemStack defaultHeldItem;

    public EntityGeistersCultist(World world)
    {
        super(world);
        texture = "/mob/geisterscultist.png";
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

    protected void attackEntity(Entity entity, float f)
    {
        if(f < 10F)
        {
            double d = entity.posX - posX;
            double d1 = entity.posZ - posZ;
            if(attackTime == 0)
            {
                EntitySnowball entitysnowball = new EntitySnowball(worldObj, this);
                double d2 = (entity.posY + (double)entity.getEyeHeight()) - 1.1000000238418579D - entitysnowball.posY;
                float f1 = MathHelper.sqrt_double(d * d + d1 * d1) * 0.2F;
                worldObj.playSoundAtEntity(this, "random.bow", 1.0F, 1.0F / (rand.nextFloat() * 0.4F + 0.8F));
                worldObj.entityJoinedWorld(entitysnowball);
                entitysnowball.setThrowableHeading(d, d2 + (double)f1, d1, 1.6F, 12F);
                attackTime = 10;
            }
            rotationYaw = (float)((Math.atan2(d1, d) * 180D) / 3.1415927410125732D) - 90F;
            hasAttacked = true;
        }
    }

    protected String getLivingSound()
    {
        return null;
    }

    protected String getHurtSound()
    {
        return "random.hurt";
    }

    protected String getDeathSound()
    {
        return "random.hurt";
    }

    protected int getDropItemId()
    {
        return Item.snowball.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.bone, 1);
    }
}
