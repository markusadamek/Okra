# Okra

**A Rust-inspired `Result` type for C# — errors as values, without leaving C# behind.**

Okra brings the `Ok` / `Err` pattern to .NET as a lightweight union. No exceptions for expected failures, no `null` sentinel checks, no boolean-return-plus-out-parameter gymnastics. Just a value that says exactly what happened, and a compiler that helps you handle it.

Named after the vegetable. It's short, it's memorable, and it starts with `Ok`.

---

## Why Okra?

Exceptions are excellent at what they were designed for: signalling that something went wrong that you did not anticipate and probably cannot recover from. They are poor at modelling outcomes you *expect* — a validation failure, a missing record, a payment decline, a 404.

Using exceptions for those cases costs you:

- **Hidden control flow.** Any method call can throw. Signatures lie about what can happen.
- **Performance.** Throwing is expensive; exceptions are not a branching primitive.
- **Non-exhaustive handling.** Nothing forces you to deal with the failure path. `catch (Exception) { }` compiles fine.
- **Awkward async composition.** Exceptions cross `await` boundaries silently and get aggregated in ways that surprise people.

Okra makes the failure path part of the type. A method that can fail says so in its signature, and the failure branch is impossible to ignore without deliberately doing so.
