package net.minecraft.src;

public class EntityGeistersMaid extends EntityVillager
{
    public EntityGeistersMaid(World world)
    {
        this(world, 0);
    }

    public EntityGeistersMaid(World world, int i)
    {
        super(world);
        texture = "/mob/geistersmaid.png";
        moveSpeed = 0.5F;
    }

    public void onLivingUpdate()
    {
        super.onLivingUpdate();
    }

    public void writeEntityToNBT(NBTTagCompound nbttagcompound)
    {
        super.writeEntityToNBT(nbttagcompound);
    }

    public void readEntityFromNBT(NBTTagCompound nbttagcompound)
    {
        super.readEntityFromNBT(nbttagcompound);
        setTextureByProfession();
    }

    private void setTextureByProfession()
    {
        texture = "/mob/geistersmaid.png";
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
        return Item.wheat.shiftedIndex;
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        ItemStack itemstack = entityplayer.inventory.getCurrentItem();
        if(itemstack != null && itemstack.itemID == Block.plantRed.blockID)
        {
            entityplayer.addChatMessage("What a nice flower! Here, take a cookie.");
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.cookie));
            return true;
        } else
        {
            entityplayer.addChatMessage("Hello there!");
            return false;
        }
    }

}
