using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "inputComponentUpdateLoop",
        Scope = "member", Target = "Base.ActiveCollection.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "ain", Scope = "member",
        Target = "Base.Components.AnalogInputItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "aout", Scope = "member",
        Target = "Base.Components.AnalogOutputItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "talon", Scope = "member",
        Target = "Base.Components.CanTalonItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "thread", Scope = "member",
        Target = "Base.ControlLoop.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "din", Scope = "member",
        Target = "Base.Components.DigitalInputItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "dout", Scope = "member",
        Target = "Base.Components.DigitalOutputItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "solenoid",
        Scope = "member", Target = "Base.Components.DoubleSolenoidItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "victor", Scope = "member",
        Target = "Base.Components.VictorItem.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Base.ControlLoop.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed",
        MessageId = "<ActiveCollection>k__BackingField", Scope = "member", Target = "Base.Config.#Dispose()")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Scope = "member",
        Target = "Base.Config.#Dispose()")]
// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.