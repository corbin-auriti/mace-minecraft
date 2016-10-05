package net.minecraft.src;

public class EntityGeistersSmith extends EntityVillager
{
    private static final ItemStack defaultHeldItem;

    public EntityGeistersSmith(World world)
    {
        this(world, 0);
    }

    public EntityGeistersSmith(World world, int i)
    {
        super(world);
        texture = "/mob/geisterssmith.png";
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
        texture = "/mob/geisterssmith.png";
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
        return Item.coal.shiftedIndex;
    }

    public ItemStack getHeldItem()
    {
        return defaultHeldItem;
    }

    static 
    {
        defaultHeldItem = new ItemStack(Item.axeStone, 1);
    }

    public boolean interact(EntityPlayer entityplayer)
    {
        ItemStack itemstack = entityplayer.inventory.getCurrentItem();
        if(itemstack != null && itemstack.itemID == Item.goldNugget.shiftedIndex)
        {
            entityplayer.addChatMessage("Some call this junk. I call it treasure.");
            int j = rand.nextInt(6);
            if(j == 1)
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.swordSteel));
            else if(j == 2)
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.helmetSteel));
            else if(j == 3)
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.plateSteel));
            else if(j == 4)
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.legsSteel));
            else if(j == 5)
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bootsSteel));
            else
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bucketEmpty));
            return true;
        } else
        {
            entityplayer.addChatMessage("Yes? Do you need a good sword or armor?");
            return false;
        }
    }

}
