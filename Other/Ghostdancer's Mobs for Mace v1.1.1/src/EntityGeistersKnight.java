package net.minecraft.src;

import java.util.List;
import java.util.Random;

public class EntityGeistersKnight extends EntityVillager
{
    private int angerLevel;
    private static final ItemStack defaultHeldItem;

    public EntityGeistersKnight(World world)
    {
        this(world, 0);
    }

    public EntityGeistersKnight(World world, int i)
    {
        super(world);
        angerLevel = 0;
        texture = "/mob/geistersknight.png";
        moveSpeed = 0.8F;
    }

    protected Entity findPlayerToAttack()
    {
        if (angerLevel == 0)
        {
            return null;
        }
        else
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
        if (entity instanceof EntityPlayer)
        {
            List list = worldObj.getEntitiesWithinAABBExcludingEntity(this, boundingBox.expand(32D, 32D, 32D));
            for (int j = 0; j < list.size(); j++)
            {
                Entity entity1 = (Entity)list.get(j);
                if (entity1 instanceof EntityGeistersKnight)
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

    public void writeEntityToNBT(NBTTagCompound nbttagcompound)
    {
        super.writeEntityToNBT(nbttagcompound);
        nbttagcompound.setShort("Anger", (short)angerLevel);
    }

    public void readEntityFromNBT(NBTTagCompound nbttagcompound)
    {
        super.readEntityFromNBT(nbttagcompound);
        angerLevel = nbttagcompound.getShort("Anger");
        setTextureByProfession();
    }

    private void setTextureByProfession()
    {
        texture = "/mob/geistersknight.png";
    }

    protected boolean canDespawn()
    {
        return false;
    }

    protected String getLivingSound()
    {
        return "mob.villager.default";
    }

    protected String getHurtSound()
    {
        return "mob.villager.defaulthurt";
    }

    protected String getDeathSound()
    {
        return "mob.villager.defaultdeath";
    }

    protected int getDropItemId()
    {
        return Item.appleRed.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.swordSteel, 1);
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        if(angerLevel == 0)
        {
            entityplayer.addChatMessage("I used to be a miner like you, but then I took a creeper to the knee.");
            return false;
        }
        else
        {
            entityplayer.addChatMessage("Stop it right there, criminal scumm!");
            return false;
        }
    }

}