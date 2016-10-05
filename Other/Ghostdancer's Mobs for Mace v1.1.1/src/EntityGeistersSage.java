package net.minecraft.src;

public class EntityGeistersSage extends EntityVillager
{
    private static final ItemStack defaultHeldItem;

    public EntityGeistersSage(World world)
    {
        this(world, 0);
    }

    public EntityGeistersSage(World world, int i)
    {
        super(world);
        texture = "/mob/geisterssage.png";
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
        texture = "/mob/geisterssage.png";
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
        return Item.paper.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.paper, 1);
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        ItemStack itemstack = entityplayer.inventory.getCurrentItem();
        if(itemstack != null && itemstack.itemID == Item.goldNugget.shiftedIndex)
        {
            entityplayer.addChatMessage("Interesting. I will give you a map for that.");
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.map));
            return true;
        } else
        {
            entityplayer.addChatMessage("Sorry, but I am busy with my research.");
            return false;
        }
    }

}
