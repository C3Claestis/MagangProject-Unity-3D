using System;

public interface IDamageable
{
    event EventHandler OnHitted;
    event EventHandler OnDead;
    
    void Damage(int damage, bool isCriticalHit);
}