package net.minecraft.src;

public class ModelGeistersGrubman extends ModelBase
{
    public ModelRenderer geistersgrubmanHead;
    public ModelRenderer geistersgrubmanHeadwear;
    public ModelRenderer geistersgrubmanBody;
    public ModelRenderer geistersgrubmanRightArm;
    public ModelRenderer geistersgrubmanLeftArm;
    public ModelRenderer geistersgrubmanRightLeg;
    public ModelRenderer geistersgrubmanLeftLeg;
    public ModelRenderer geistersgrubmanEars;
    public ModelRenderer geistersgrubmanCloak;
    public int heldItemLeft;
    public int heldItemRight;
    public boolean isSneak;
    public boolean aimedBow;

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
        heldItemLeft = 0;
        heldItemRight = 0;
        isSneak = false;
        aimedBow = false;
        geistersgrubmanCloak = new ModelRenderer(this, 0, 0);
        geistersgrubmanCloak.addBox(-5F, 0.0F, -1F, 10, 16, 1, f);

        geistersgrubmanEars = new ModelRenderer(this, 24, 0);
        geistersgrubmanEars.addBox(-3F, -6F, -1F, 6, 6, 1, f);

        geistersgrubmanHead = new ModelRenderer(this, 0, 0);
        geistersgrubmanHead.addBox(-4F, 2F, -4F, 8, 6, 8, f);
        geistersgrubmanHead.setRotationPoint(0.0F, 0.0F + f1, 0.0F);

        geistersgrubmanHeadwear = new ModelRenderer(this, 32, 0);
        geistersgrubmanHeadwear.addBox(-4F, 2F, -4F, 8, 6, 8, f + 0.5F);
        geistersgrubmanHeadwear.setRotationPoint(0.0F, 0.0F +f1, 0.0F);

        geistersgrubmanBody = new ModelRenderer(this, 16, 16);
        geistersgrubmanBody.addBox(-4F, 8.0F, -2F, 8, 8, 4, f);
        geistersgrubmanBody.setRotationPoint(0.0F, 0.0F +f1, 0.0F);

        geistersgrubmanRightArm = new ModelRenderer(this, 40, 16);
        geistersgrubmanRightArm.addBox(-3F, 4F, -2F, 4, 12, 4, f);
        geistersgrubmanRightArm.setRotationPoint(-5F, 2F +f1, 0.0F);

        geistersgrubmanLeftArm = new ModelRenderer(this, 40, 16);
        geistersgrubmanLeftArm.mirror = true;
        geistersgrubmanLeftArm.addBox(-1F, 4F, -2F, 4, 12, 4, f);
        geistersgrubmanLeftArm.setRotationPoint(5F, 2F +f1, 0.0F);

        geistersgrubmanRightLeg = new ModelRenderer(this, 0, 16);
        geistersgrubmanRightLeg.addBox(-2F, 4.0F, -2F, 4, 8, 4, f);
        geistersgrubmanRightLeg.setRotationPoint(-2F, 12F +f1, 0.0F);

        geistersgrubmanLeftLeg = new ModelRenderer(this, 0, 16);
        geistersgrubmanLeftLeg.mirror = true;
        geistersgrubmanLeftLeg.addBox(-2F, 4F, -2F, 4, 8, 4, f);
        geistersgrubmanLeftLeg.setRotationPoint(2F, 12F +f1, 0.0F);
    }

    public void render(Entity entity, float f, float f1, float f2, float f3, float f4, float f5)
    {
        setRotationAngles(f, f1, f2, f3, f4, f5);
        geistersgrubmanHead.render(f5);
        geistersgrubmanBody.render(f5);
        geistersgrubmanRightArm.render(f5);
        geistersgrubmanLeftArm.render(f5);
        geistersgrubmanRightLeg.render(f5);
        geistersgrubmanLeftLeg.render(f5);
        geistersgrubmanHeadwear.render(f5);
    }

    public void setRotationAngles(float f, float f1, float f2, float f3, float f4, float f5)
    {
        geistersgrubmanHead.rotateAngleY = f3 / 57.29578F;
        geistersgrubmanHead.rotateAngleX = f4 / 57.29578F;
        geistersgrubmanHeadwear.rotateAngleY = geistersgrubmanHead.rotateAngleY;
        geistersgrubmanHeadwear.rotateAngleX = geistersgrubmanHead.rotateAngleX;
        geistersgrubmanRightArm.rotateAngleX = MathHelper.cos(f * 0.6662F + 3.141593F) * 2.0F * f1 * 0.5F;
        geistersgrubmanLeftArm.rotateAngleX = MathHelper.cos(f * 0.6662F) * 2.0F * f1 * 0.5F;
        geistersgrubmanRightArm.rotateAngleZ = 0.0F;
        geistersgrubmanLeftArm.rotateAngleZ = 0.0F;
        geistersgrubmanRightLeg.rotateAngleX = MathHelper.cos(f * 0.6662F) * 1.4F * f1;
        geistersgrubmanLeftLeg.rotateAngleX = MathHelper.cos(f * 0.6662F + 3.141593F) * 1.4F * f1;
        geistersgrubmanRightLeg.rotateAngleY = 0.0F;
        geistersgrubmanLeftLeg.rotateAngleY = 0.0F;
        if (isRiding)
        {
            geistersgrubmanRightArm.rotateAngleX += -0.6283185F;
            geistersgrubmanLeftArm.rotateAngleX += -0.6283185F;
            geistersgrubmanRightLeg.rotateAngleX = -1.256637F;
            geistersgrubmanLeftLeg.rotateAngleX = -1.256637F;
            geistersgrubmanRightLeg.rotateAngleY = 0.3141593F;
            geistersgrubmanLeftLeg.rotateAngleY = -0.3141593F;
        }
        if (heldItemLeft != 0)
        {
            geistersgrubmanLeftArm.rotateAngleX = geistersgrubmanLeftArm.rotateAngleX * 0.5F - 0.3141593F * (float)heldItemLeft;
        }
        if (heldItemRight != 0)
        {
            geistersgrubmanRightArm.rotateAngleX = geistersgrubmanRightArm.rotateAngleX * 0.5F - 0.3141593F * (float)heldItemRight;
        }
        geistersgrubmanRightArm.rotateAngleY = 0.0F;
        geistersgrubmanLeftArm.rotateAngleY = 0.0F;
        if (onGround > -9990F)
        {
            float f6 = onGround;
            geistersgrubmanBody.rotateAngleY = MathHelper.sin(MathHelper.sqrt_float(f6) * 3.141593F * 2.0F) * 0.2F;
            geistersgrubmanRightArm.rotationPointZ = MathHelper.sin(geistersgrubmanBody.rotateAngleY) * 5F;
            geistersgrubmanRightArm.rotationPointX = -MathHelper.cos(geistersgrubmanBody.rotateAngleY) * 5F;
            geistersgrubmanLeftArm.rotationPointZ = -MathHelper.sin(geistersgrubmanBody.rotateAngleY) * 5F;
            geistersgrubmanLeftArm.rotationPointX = MathHelper.cos(geistersgrubmanBody.rotateAngleY) * 5F;
            geistersgrubmanRightArm.rotateAngleY += geistersgrubmanBody.rotateAngleY;
            geistersgrubmanLeftArm.rotateAngleY += geistersgrubmanBody.rotateAngleY;
            geistersgrubmanLeftArm.rotateAngleX += geistersgrubmanBody.rotateAngleY;
            f6 = 1.0F - onGround;
            f6 *= f6;
            f6 *= f6;
            f6 = 1.0F - f6;
            float f8 = MathHelper.sin(f6 * 3.141593F);
            float f10 = MathHelper.sin(onGround * 3.141593F) * -(geistersgrubmanHead.rotateAngleX - 0.7F) * 0.75F;
            geistersgrubmanRightArm.rotateAngleX -= (double)f8 * 1.2D + (double)f10;
            geistersgrubmanRightArm.rotateAngleY += geistersgrubmanBody.rotateAngleY * 2.0F;
            geistersgrubmanRightArm.rotateAngleZ = MathHelper.sin(onGround * 3.141593F) * -0.4F;
        }
        if (isSneak)
        {
            geistersgrubmanBody.rotateAngleX = 0.5F;
            geistersgrubmanRightLeg.rotateAngleX -= 0.0F;
            geistersgrubmanLeftLeg.rotateAngleX -= 0.0F;
            geistersgrubmanRightArm.rotateAngleX += 0.4F;
            geistersgrubmanLeftArm.rotateAngleX += 0.4F;
            geistersgrubmanRightLeg.rotationPointZ = 4F;
            geistersgrubmanLeftLeg.rotationPointZ = 4F;
            geistersgrubmanRightLeg.rotationPointY = 9F;
            geistersgrubmanLeftLeg.rotationPointY = 9F;
            geistersgrubmanHead.rotationPointY = 1.0F;
        }
        else
        {
            geistersgrubmanBody.rotateAngleX = 0.0F;
            geistersgrubmanRightLeg.rotationPointZ = 0.0F;
            geistersgrubmanLeftLeg.rotationPointZ = 0.0F;
            geistersgrubmanRightLeg.rotationPointY = 12F;
            geistersgrubmanLeftLeg.rotationPointY = 12F;
            geistersgrubmanHead.rotationPointY = 0.0F;
        }
        geistersgrubmanRightArm.rotateAngleZ += MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
        geistersgrubmanLeftArm.rotateAngleZ -= MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
        geistersgrubmanRightArm.rotateAngleX += MathHelper.sin(f2 * 0.067F) * 0.05F;
        geistersgrubmanLeftArm.rotateAngleX -= MathHelper.sin(f2 * 0.067F) * 0.05F;
        if (aimedBow)
        {
            float f7 = 0.0F;
            float f9 = 0.0F;
            geistersgrubmanRightArm.rotateAngleZ = 0.0F;
            geistersgrubmanLeftArm.rotateAngleZ = 0.0F;
            geistersgrubmanRightArm.rotateAngleY = -(0.1F - f7 * 0.6F) + geistersgrubmanHead.rotateAngleY;
            geistersgrubmanLeftArm.rotateAngleY = (0.1F - f7 * 0.6F) + geistersgrubmanHead.rotateAngleY + 0.4F;
            geistersgrubmanRightArm.rotateAngleX = -1.570796F + geistersgrubmanHead.rotateAngleX;
            geistersgrubmanLeftArm.rotateAngleX = -1.570796F + geistersgrubmanHead.rotateAngleX;
            geistersgrubmanRightArm.rotateAngleX -= f7 * 1.2F - f9 * 0.4F;
            geistersgrubmanLeftArm.rotateAngleX -= f7 * 1.2F - f9 * 0.4F;
            geistersgrubmanRightArm.rotateAngleZ += MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
            geistersgrubmanLeftArm.rotateAngleZ -= MathHelper.cos(f2 * 0.09F) * 0.05F + 0.05F;
            geistersgrubmanRightArm.rotateAngleX += MathHelper.sin(f2 * 0.067F) * 0.05F;
            geistersgrubmanLeftArm.rotateAngleX -= MathHelper.sin(f2 * 0.067F) * 0.05F;
        }
    }

    public void renderEars(float f)
    {
        geistersgrubmanEars.rotateAngleY = geistersgrubmanHead.rotateAngleY;
        geistersgrubmanEars.rotateAngleX = geistersgrubmanHead.rotateAngleX;
        geistersgrubmanEars.rotationPointX = 0.0F;
        geistersgrubmanEars.rotationPointY = 0.0F;
        geistersgrubmanEars.render(f);
    }

    public void renderCloak(float f)
    {
        geistersgrubmanCloak.render(f);
    }
}
