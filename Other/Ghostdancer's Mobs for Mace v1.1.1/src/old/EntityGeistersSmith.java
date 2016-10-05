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

public class EntityGeistersSmith extends EntityAnimal
{

    private int angerLevel;
    private static final ItemStack defaultHeldItem;

    public EntityGeistersSmith(World world)
    {
        super(world);
        angerLevel = 0;
        texture = "/mob/geisterssmith.png";
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
// Wood Tools
        if(itemstack != null && itemstack.itemID == Item.swordWood.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.swordWood));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.shovelWood.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.shovelWood));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.pickaxeWood.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.pickaxeWood));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.axeWood.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.axeWood));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.hoeWood.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.hoeWood));
            return true;
// Stone Tools
        } else if(itemstack != null && itemstack.itemID == Item.swordStone.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.swordStone));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.shovelStone.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.shovelStone));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.pickaxeStone.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.pickaxeStone));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.axeStone.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.axeStone));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.hoeStone.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.hoeStone));
            return true;
// Iron Tools
        } else if(itemstack != null && itemstack.itemID == Item.swordSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.swordSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.shovelSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.shovelSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.pickaxeSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.pickaxeSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.axeSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.axeSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.hoeSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.hoeSteel));
            return true;
// Leather Armor
        } else if(itemstack != null && itemstack.itemID == Item.helmetLeather.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.helmetLeather));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.plateLeather.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.plateLeather));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.legsLeather.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.legsLeather));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.bootsLeather.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bootsLeather));
            return true;
// Chain Armor
        } else if(itemstack != null && itemstack.itemID == Item.helmetChain.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.helmetChain));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.plateChain.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.plateChain));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.legsChain.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.legsChain));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.bootsChain.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bootsChain));
            return true;
// Iron Armor
        } else if(itemstack != null && itemstack.itemID == Item.helmetSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.helmetSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.plateSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.plateSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.legsSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.legsSteel));
            return true;
        } else if(itemstack != null && itemstack.itemID == Item.bootsSteel.shiftedIndex)
        {
            entityplayer.inventory.setInventorySlotContents(entityplayer.inventory.currentItem, new ItemStack(Item.bootsSteel));
            return true;
        } else
        {
            return false;
        }
    }
}
