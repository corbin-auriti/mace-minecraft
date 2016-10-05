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

public class EntityGeistersMaid extends EntityAnimal
{

    private int angerLevel;

    public EntityGeistersMaid(World world)
    {
        super(world);
        angerLevel = 0;
        texture = "/mob/geistersmaid.png";
    }

    public int getMaxHealth()
    {
        return 20;
    }

    protected EntityAnimal func_40145_a(EntityAnimal entityanimal)
    {
        return null;
    }

    public void writeEntityToNBT(NBTTagCompound nbttagcompound)
    {
        super.writeEntityToNBT(nbttagcompound);
        nbttagcompound.setShort("Anger", (short)angerLevel);
    }

    public void readEntityFromNBT(NBTTagCompound nbttagcompound)
    {
        super.readEntityFromNBT(nbttagcompound);
        angerLevel = nbttagcompound.getShort("Anger");
    }

    protected Entity findPlayerToAttack()
    {
        if(angerLevel == 0)
        {
            return null;
        } else
        {
            return super.findPlayerToAttack();
        }
    }

    public void onLivingUpdate()
    {
        super.onLivingUpdate();
    }

    public boolean attackEntityFrom(DamageSource damagesource, int i)
    {
        Entity entity = damagesource.getEntity();
        if(entity instanceof EntityPlayer)
        {
            List list = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(32D, 32D, 32D));
            for(int j = 0; j < list.size(); j++)
            {
                Entity entity1 = (Entity)list.get(j);
                if(entity1 instanceof EntityGeistersKnight)
                {
                    EntityGeistersKnight entitygeistersknight = (EntityGeistersKnight)entity1;
                    entitygeistersknight.becomeAngryAt(entity);
                }
            }

            becomeAngryAt(entity);
        }
        return super.attackEntityFrom(damagesource, i);
    }

    private void becomeAngryAt(Entity entity)
    {
        entityToAttack = entity;
        angerLevel = 400 + rand.nextInt(400);
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
        return Item.wheat.shiftedIndex;
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        ItemStack itemstack = entityplayer.inventory.getCurrentItem();
        if(itemstack != null && itemstack.itemID == Block.plantRed.blockID)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bread));
            return true;
        } else
        {
            return false;
        }
    }

}
