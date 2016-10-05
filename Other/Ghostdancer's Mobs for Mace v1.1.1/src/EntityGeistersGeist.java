package net.minecraft.src;

import java.util.Random;

public class EntityGeistersGeist extends EntityZombie
{
    public EntityGeistersGeist(World world)
    {
        super(world);
        texture = "/mob/geistersgeist.png";
        moveSpeed = 0.5F;
        attackStrength = 4;
    }

    protected int getDropItemId()
    {
        return Item.slimeBall.shiftedIndex;
    }

}
