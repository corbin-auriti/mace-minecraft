package net.minecraft.src;

import java.util.List;
import java.util.Random;

public class EntityGeistersRogue extends EntityVillager
{
    private int angerLevel;
    private static final ItemStack defaultHeldItem;

    public EntityGeistersRogue(World world)
    {
        this(world, 0);
    }

    public EntityGeistersRogue(World world, int i)
    {
        super(world);
        angerLevel = 400;
        texture = "/mob/geistersrogue.png";
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
                if (entity1 instanceof EntityGeistersRogue)
                {
                    EntityGeistersRogue entitygeistersrogue = (EntityGeistersRogue)entity1;
                    entitygeistersrogue.becomeAngryAt(entity);
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
        texture = "/mob/geistersrogue.png";
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

    protected void attackEntity(Entity entity, float f)
    {
        if (f < 10F)
        {
            double d = entity.posX - posX;
            double d1 = entity.posZ - posZ;
            if (attackTime == 0)
            {
                EntityArrow entityarrow = new EntityArrow(worldObj, this, 1.0F);
                double d2 = (entity.posY + (double)entity.getEyeHeight()) - 0.69999998807907104D - entityarrow.posY;
                float f1 = MathHelper.sqrt_double(d * d + d1 * d1) * 0.2F;
                worldObj.playSoundAtEntity(this, "random.bow", 1.0F, 1.0F / (rand.nextFloat() * 0.4F + 0.8F));
                worldObj.spawnEntityInWorld(entityarrow);
                entityarrow.setArrowHeading(d, d2 + (double)f1, d1, 1.6F, 12F);
                attackTime = 60;
            }
            rotationYaw = (float)((Math.atan2(d1, d) * 180D) / 3.1415927410125732D) - 90F;
            hasAttacked = true;
        }
    }

    protected int getDropItemId()
    {
        return Item.arrow.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.bow, 1);
    }

}
