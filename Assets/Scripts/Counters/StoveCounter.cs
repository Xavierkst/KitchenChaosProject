using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    [SerializeField] private CookingRecipeSO[] cookingRecipeSOArray;
    [SerializeField] private BurnedRecipeSO[] burnedRecipeSOArray;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    private float cookingProgress;
    private float burnedProgress;
    private CookingRecipeSO cookingRecipeSO;
    private BurnedRecipeSO burnedRecipeSO;
    private State state;

    public enum State {
        Idle,
        Cooking,
        Cooked, 
        Burned
    }
    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch(state) {
                case State.Idle:
                    break;
                case State.Cooking:
                    cookingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = cookingProgress / cookingRecipeSO.cookingProgressMax
                    });
                    if (cookingProgress > cookingRecipeSO.cookingProgressMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this, GetCookingOutputForInput(cookingRecipeSO.input));
                        state = State.Cooked;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });
                        // swap uncooked to cooked patty CookingRecipeSO
                        burnedRecipeSO = GetBurnedRecipeSOWithInput(cookingRecipeSO.output);
                        burnedProgress = 0f;
                    }
                    break;
                case State.Cooked:
                    burnedProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burnedProgress / burnedRecipeSO.burnedProgressMax
                    });
                    if (burnedProgress > burnedRecipeSO.burnedProgressMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(this, GetBurnedOutputForInput(burnedRecipeSO.input));
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // Check that kitchen object has an output for its input -- 
            // should let player put cooked patty if he/she wants to
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cookingRecipeSO = GetCookingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                if (cookingRecipeSO) {
                    state = State.Cooking;
                    cookingProgress = 0f;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = cookingProgress / cookingRecipeSO.cookingProgressMax
                    });
                } else if (burnedRecipeSO = GetBurnedRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO())){
                    state = State.Cooked;
                    burnedProgress = 0f;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = burnedProgress / burnedRecipeSO.burnedProgressMax
                    });
                }

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = this.state
                });
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        state = State.Idle;
                        GetKitchenObject().DestroySelf();
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                            state = this.state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = this.state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    public CookingRecipeSO GetCookingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CookingRecipeSO cookingRecipeSO in cookingRecipeSOArray) {
            if (cookingRecipeSO.input == inputKitchenObjectSO) {
                return cookingRecipeSO;
            }
        }
        return null;
    }

    public BurnedRecipeSO GetBurnedRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurnedRecipeSO burnedRecipeSO in burnedRecipeSOArray) {
            if (burnedRecipeSO.input == inputKitchenObjectSO) {
                return burnedRecipeSO;
            }
        }
        return null;
    }


    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        return GetCookingRecipeSOWithInput(inputKitchenObjectSO) != null || 
            GetBurnedRecipeSOWithInput(inputKitchenObjectSO) != null;
    }

    public KitchenObjectSO GetCookingOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CookingRecipeSO cookingRecipeSO = GetCookingRecipeSOWithInput(inputKitchenObjectSO);
        return cookingRecipeSO ? cookingRecipeSO.output : null;
    }

    public KitchenObjectSO GetBurnedOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        BurnedRecipeSO burnedRecipeSO = GetBurnedRecipeSOWithInput(inputKitchenObjectSO);
        return burnedRecipeSO ? burnedRecipeSO.output : null;
    }

}
