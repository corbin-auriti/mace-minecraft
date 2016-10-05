// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode 

package net.minecraft.src;

import org.lwjgl.opengl.GL11;

// Referenced classes of package net.minecraft.src:
//            RenderLiving, EntityLiving, ModelBiped, ModelRenderer, 
//            ItemStack, Block, RenderBlocks, Item, 
//            RenderManager, ItemRenderer

public class RenderGeistersGrubman extends RenderLiving
{

    protected ModelGeistersGrubman modelGeistersGrubmanMain;
    protected float field_40296_d;

   public RenderGeistersGrubman(ModelGeistersGrubman modelgeistersgrubman, float f)
    {
        this(modelgeistersgrubman, f, 1.0F);
        modelGeistersGrubmanMain = modelgeistersgrubman;
    }

    public RenderGeistersGrubman(ModelGeistersGrubman modelgeistersgrubman, float f, float f1)
    {
        super(modelgeistersgrubman, f);
        modelGeistersGrubmanMain = modelgeistersgrubman;
        field_40296_d = f1;
    }

    protected void renderEquippedItems(EntityLiving entityliving, float f)
    {
        super.renderEquippedItems(entityliving, f);
        ItemStack itemstack = entityliving.getHeldItem();
        if(itemstack != null)
        {
            GL11.glPushMatrix();
            modelGeistersGrubmanMain.grubmanRightArm.postRender(0.0625F);
            GL11.glTranslatef(-0.0625F, 0.6375F, 0.0625F);
            if(itemstack.itemID < 256 && RenderBlocks.renderItemIn3d(Block.blocksList[itemstack.itemID].getRenderType()))
            {
                float f1 = 0.5F;
                GL11.glTranslatef(0.0F, 0.3875F, -0.3125F);
                f1 *= 0.75F;
                GL11.glRotatef(20F, 1.0F, 0.0F, 0.0F);
                GL11.glRotatef(45F, 0.0F, 1.0F, 0.0F);
                GL11.glScalef(f1, -f1, f1);
            } else
            if(itemstack.itemID == Item.bow.shiftedIndex)
            {
                float f2 = 0.625F;
                GL11.glTranslatef(0.0F, 0.325F, 0.3125F);
                GL11.glRotatef(-20F, 0.0F, 1.0F, 0.0F);
                GL11.glScalef(f2, -f2, f2);
                GL11.glRotatef(-100F, 1.0F, 0.0F, 0.0F);
                GL11.glRotatef(45F, 0.0F, 1.0F, 0.0F);
            } else
            if(Item.itemsList[itemstack.itemID].isFull3D())
            {
                float f3 = 0.625F;
                GL11.glTranslatef(0.0F, 0.3875F, 0.0F);
                GL11.glScalef(f3, -f3, f3);
                GL11.glRotatef(-100F, 1.0F, 0.0F, 0.0F);
                GL11.glRotatef(45F, 0.0F, 1.0F, 0.0F);
            } else
            {
                float f4 = 0.375F;
                GL11.glTranslatef(0.25F, 0.3875F, -0.1875F);
                GL11.glScalef(f4, f4, f4);
                GL11.glRotatef(60F, 0.0F, 0.0F, 1.0F);
                GL11.glRotatef(-90F, 1.0F, 0.0F, 0.0F);
                GL11.glRotatef(20F, 0.0F, 0.0F, 1.0F);
            }
            renderManager.itemRenderer.renderItem(entityliving, itemstack, 0);
            if(itemstack.itemID == Item.bow.shiftedIndex)
            {
                renderManager.itemRenderer.renderItem(entityliving, itemstack, 1);
            }
            GL11.glPopMatrix();
        }
    }

}
