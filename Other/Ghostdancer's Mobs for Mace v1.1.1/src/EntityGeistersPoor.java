package net.minecraft.src;

public class EntityGeistersPoor extends EntityVillager
{
    public EntityGeistersPoor(World world)
    {
        this(world, 0);
    }

    public EntityGeistersPoor(World world, int i)
    {
        super(world);
        texture = "/mob/geisterspoor.png";
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
        texture = "/mob/geisterspoor.png";
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
        if(itemstack != null && itemstack.itemID == Item.goldNugget.shiftedIndex)
        {
            entityplayer.addChatMessage("Thanks, sir. I ain't have much, but take these.");
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.melonSeeds));
            return true;
        } else
        {
            entityplayer.addChatMessage("Uhm... yes?");
            return false;
        }
    }

}
