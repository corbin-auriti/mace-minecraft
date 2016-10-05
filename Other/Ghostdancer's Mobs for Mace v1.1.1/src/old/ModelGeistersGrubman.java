// Decompiled by Jad v1.5.8g. Copyright 2001 Pavel Kouznetsov.
// Jad home page: http://www.kpdus.com/jad.html
// Decompiler options: packimports(3) braces deadcode 

package net.minecraft.src;


// Referenced classes of package net.minecraft.src:
//            ModelBase, ModelRenderer, MathHelper, Entity

public class ModelGeistersGrubman extends ModelBase
{

    public ModelGeistersGrubman()
    {
        this(0.0F);
    }

    public ModelGeistersGrubman(float f)
    {
        this(f, 0.0F);
    }

    public ModelGeistersGrubman(float f, float f1)
    {
        field_1279_h = false;
        field_1278_i = false;
        isSneak = false;
        grubmanCloak = new ModelRenderer(this, 0, 0);
        grubmanCloak.addBox(-5F, 0.0F, -1F, 10, 16, 1, f);

        grubmanEars = new ModelRenderer(this, 24, 0);
        grubmanEars.addBox(-3F, -6F, -1F, 6, 6, 1, f);

        grubmanHead = new ModelRenderer(this, 0, 0);
        grubmanHead.addBox(-4F, 2F, -4F, 8, 6, 8, f);
        grubmanHead.setRotationPoint(0.0F, 0.0F + f1, 0.0F);

        grubmanHeadwear = new ModelRenderer(this, 32, 0);
        grubmanHeadwear.addBox(-4F, 2F, -4F, 8, 6, 8, f + 0.5F);
        grubmanHeadwear.setRotationPoint(0.0F, 0.0F + f1, 0.0F);

        grubmanBody = new ModelRenderer(this, 16, 16);
        grubmanBody.addBox(-4F, 8.0F, -2F, 8, 8, 4, f);
        grubmanBody.setRotationPoint(0.0F, 0.0F + f1, 0.0F);

        grubmanRightArm = new ModelRenderer(this, 40, 16);
        grubmanRightArm.addBox(-3F, 4F, -2F, 4, 12, 4, f);
        grubmanRightArm.setRotationPoint(-5F, 2.0F + f1, 0.0F);

        grubmanLeftArm = new ModelRenderer(this, 40, 16);
        grubmanLeftArm.mirror = true;
        grubmanLeftArm.addBox(-1F, 4F, -2F, 4, 12, 4, f);
        grubmanLeftArm.setRotationPoint(5F, 2.0F + f1, 0.0F);

        grubmanRightLeg = new ModelRenderer(this, 0, 16);
        grubmanRightLeg.addBox(-2F, 4F, -2F, 4, 8, 4, f);
        grubmanRightLeg.setRotationPoint(-2F, 12F + f1, 0.0F);

        grubmanLeftLeg = new ModelRenderer(this, 0, 16);
        grubmanLeftLeg.mirror = true;
        grubmanLeftLeg.addBox(-2F, 4F, -2F, 4, 8, 4, f);
        grubmanLeftLeg.setRotationPoint(2.0F, 12F + f1, 0.0F);
    }

    public void render(Entity entity, float f, float f1, float f2, float f3, float f4, float f5)
    {
        setRotationAngles(f, f1, f2, f3, f4, f5);
        grubmanHead.render(f5);
        grubmanBody.render(f5);
        grubmanRightArm.render(f5);
        grubmanLeftArm.render(f5);
        grubmanRightLeg.render(f5);
        grubmanLeftLeg.render(f5);
        grubmanHeadwear.render(f5);
    }

    public void setRotationAngles(float f, float f1, float f2, float f3, float f4, float f5)
    {
        grubmanHead.rotateAngleY = f3 / 57.29578F;
        grubmanHead.rotateAngleX = f4 / 57.29578F;
        grubmanHeadwear.rotateAngleY = grubmanHead.rotateAngleY;
        grubmanHeadwear.rotateAngleX = grubmanHead.rotateAngleX;
        grubmanRightArm.rotateAngleX = MathHelper.cos(f * 0.6662F + 3.141593F) * 2.0F * f1 * 0.5F;
        grubmanLeftArm.rotateAngleX = MathHelper.cos(f * 0.6662F) * 2.0F * f1 * 0.5F;
        grubmanRightArm.rotateAngleZ = 0.0F;
        grubmanLeftArm.rotateAngleZ = 0.0F;
        grubmanRightLeg.rotateAngleX = MathHelper.cos(f * 0.6662F) * 1.4F * f1;
        grubmanLeftLeg.rotateAngleX = MathHelper.cos(f * 0.6662F + 3.141593F) * 1.4F * f1;
        grubmanRightLeg.rotateAngleY = 0.0F;
        grubmanLeftLeg.rotateAngleY = 0.0F;
        if(isRiding)
        {
            grubmanRightArm.rotateAngleX += -0.6283185F;
            grubmanLeftArm.rotateAngleX += -0.6283185F;
            grubmanRightLeg.rotateAngleX = -1.256637F;
            grubmanLeftLeg.rotateAngleX = -1.256637F;
            grubmanRightLeg.rotateAngleY = 0.3141593F;
            grubmanLeftLeg.rotateAngleY = -0.3141593F;
        }
        if(field_1279_h)
        {
            grubmanLeftArm.rotateAngleX = grubmanLeftArm.rotateAngleX * 0.5F - 0.3141593F;
        }
        if(field_1278_i)
        {
            grubmanRightArm.rotateAngleX = grubmanRightArm.rotateAngleX * 0.5F - 0.3141593F;
        }
        grubmanRightArm.rotateAngleY = 0.0F;
        grubmanLeftArm.rotateAngleY = 0.0F;
        if(onGround > -9990F)
        {
            float f6 = onGround;
            grubmanBody.rotateAngleY = MathHelper.sin(MathHelper.sqrt_float(f6) * 3.141593F * 2.0F) * 0.2F;
            grubmanRightArm.rotationPointZ = MathHelper.sin(grubmanBody.rotateAngleY) * 5F;
            grubmanRightArm.rotationPointX = -MathHelper.cos(grubmanBody.rotateAngleY) * 5F;
            grubmanLeftArm.rotationPointZ = -MathHelper.sin(grubmanBody.rotateAngleY) * 5F;
            grubmanLeftArm.rotationPointX = MathHelper.cos(grubmanBody.rotateAngleY) * 5F;
            grubmanRightArm.rotateAngleY += grubmanBody.rotateAngleY;
            grubmanLeftArm.rotateAngleY += grubmanBody.rotateAngleY;
            grubmanLeftArm.rotateAngleX += grubmanBody.rotateAngleY;
            f6 = 1.0F - onGround;
            f6 *= f6;
            f6 *= f6;
            f6 = 1.0F - f6;
            float f7 = MathHelper.sin(f6 * 3.141593F);
            float f8 = MathHelper.sin(onGround * 3.141593F) * -(grubmanHead.rotateAngleX - 0.7F) * 0.75F;
            grubmanRightArm.rotateAngleX -= (double)f7 * 1.2D + (double)f8;
            grubmanRightArm.rotateAngleY += grubmanBody.rotateAngleY * 2.0F;
            grubmanRightArm.rotateAngleZ = MathHelper.sin(onGround * 3.141593F) * -0.4F;
        }
        if(isSneak)
        {
            grubmanBody.rotateAngleX = 0.5F;
            grubmanRightLeg.rotateAngleX -= 0.0F;
            grubmanLeftLeg.rotateAngleX -= 0.0F;
            grubmanRightArm.rotateAngleX += 0.4F;
            grubmanLeftArm.rotateAngleX += 0.4F;
            grubmanRightLeg.rotationPointZ = 4F;
            grubmanLeftLeg.rotationPointZ = 4F;
            grubmanRightLeg.rotationPointY = 9F;
            grubmanLeftLeg.rotationPointY = 9F;
            grubmanHead.rotationPointY = 1.0F;
        } else
        {
            grubmanBody.rotateAngleX = 0.0F;
            grubmanRightLeg.rotationPointZ = 0.0F;
            grubmanLeftLeg.rotationPointZ = 0.0F;
            grubmanRightLeg.rotationPointY = 12F;
            grubmanLeftLeg.rotationPointY = 12F;
            grubmanHead.rotationPointY = 0.0F;
        }
        grubmanRightArm.rotateAngleZ += MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
        grubmanLeftArm.rotateAngleZ -= MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
        grubmanRightArm.rotateAngleX += MathHelper.sin(f2 * 0.067F) * 0.05F;
        grubmanLeftArm.rotateAngleX -= MathHelper.sin(f2 * 0.067F) * 0.05F;
    }

    public void renderEars(float f)
    {
        grubmanEars.rotateAngleY = grubmanHead.rotateAngleY;
        grubmanEars.rotateAngleX = grubmanHead.rotateAngleX;
        grubmanEars.rotationPointX = 0.0F;
        grubmanEars.rotationPointY = 0.0F;
        grubmanEars.render(f);
    }

    public void renderCloak(float f)
    {
        grubmanCloak.render(f);
    }

    public ModelRenderer grubmanHead;
    public ModelRenderer grubmanHeadwear;
    public ModelRenderer grubmanBody;
    public ModelRenderer grubmanRightArm;
    public ModelRenderer grubmanLeftArm;
    public ModelRenderer grubmanRightLeg;
    public ModelRenderer grubmanLeftLeg;
    public ModelRenderer grubmanEars;
    public ModelRenderer grubmanCloak;
    public boolean field_1279_h;
    public boolean field_1278_i;
    public boolean isSneak;
}
