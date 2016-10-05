package net.minecraft.src;

import java.util.Random;

public class EntityGeistersGrim extends EntityZombie
{
    private static final ItemStack defaultHeldItem;

    public EntityGeistersGrim(World world)
    {
        super(world);
        texture = "/mob/geistersgrim.png";
        moveSpeed = 0.5F;
        attackStrength = 4;
    }

    public void setEntityDead()
    {
        if (!worldObj.multiplayerWorld && getEntityHealth() <= 0)
        {
            int j = 2 + rand.nextInt(3);
            for (int k = 0; k < j; k++)
            {
                float f = (((float)(k % 2) - 0.5F) * 1) / 4F;
                float f1 = (((float)(k / 2) - 0.5F) * 1) / 4F;
                EntityGeistersGeist entitygeistersgeist = new EntityGeistersGeist(worldObj);
                entitygeistersgeist.setLocationAndAngles(posX + (double)f, posY + 0.5D, posZ + (double)f1, rand.nextFloat() * 360F, 0.0F);
                worldObj.spawnEntityInWorld(entitygeistersgeist);
            }
        }
        super.setEntityDead();
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

}
