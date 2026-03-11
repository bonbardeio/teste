import test from 'node:test';
import assert from 'node:assert/strict';
import { canAfford, applyPurchase } from '../src/economy.js';

test('canAfford valida saldo', () => {
  assert.equal(canAfford(100, 20), true);
  assert.equal(canAfford(10, 20), false);
});

test('applyPurchase debita moedas corretamente', () => {
  assert.equal(applyPurchase(100, 60), 40);
  assert.throws(() => applyPurchase(10, 99));
});
