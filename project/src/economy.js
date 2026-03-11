/**
 * Módulo de economia do jogo: valida compras e calcula saldo.
 */
export function canAfford(balance, price) {
  return Number.isFinite(balance) && Number.isFinite(price) && balance >= price;
}

export function applyPurchase(balance, price) {
  if (!canAfford(balance, price)) {
    throw new Error('Saldo insuficiente para compra.');
  }
  return balance - price;
}
