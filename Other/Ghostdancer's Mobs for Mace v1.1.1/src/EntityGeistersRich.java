package net.minecraft.src;

public class EntityGeistersRich extends EntityVillager
{
    private static final ItemStack defaultHeldItem;

    public EntityGeistersRich(World world)
    {
        this(world, 0);
    }

    public EntityGeistersRich(World world, int i)
    {
        super(world);
        texture = "/mob/geistersrich.png";
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
        texture = "/mob/geistersrich.png";
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
        return Item.goldNugget.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.pocketSundial, 1);
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        ItemStack itemstack = entityplayer.inventory.getCurrentItem();
        if(itemstack != null && itemstack.itemID == Item.painting.shiftedIndex)
        {
            entityplayer.addChatMessage("An original Junkboy! I will give you this gold nugget for that.");
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.goldNugget));
            return true;
        } else
        {
            entityplayer.addChatMessage("What do you want?");
            return false;
        }
    }

}
