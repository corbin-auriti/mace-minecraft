package net.minecraft.src;

import java.util.List;
import java.util.Random;

public class EntityGeistersGrubman extends EntityVillager
{
    private int angerLevel;
    private static final ItemStack defaultHeldItem;

    public EntityGeistersGrubman(World world)
    {
        this(world, 0);
    }

    public EntityGeistersGrubman(World world, int i)
    {
        super(world);
        angerLevel = 0;
        texture = "/mob/geistersgrubman.png";
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
                if (entity1 instanceof EntityGeistersGrubman)
                {
                    EntityGeistersGrubman entitygeistersgrubman = (EntityGeistersGrubman)entity1;
                    entitygeistersgrubman.becomeAngryAt(entity);
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
        texture = "/mob/geistersgrubman.png";
    }

    protected boolean canDespawn()
    {
        return false;
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
