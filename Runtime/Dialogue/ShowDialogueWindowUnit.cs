using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Playmeld
{
    [UnitTitle("ShowDialogueWindow")]
    [UnitCategory("Playmeld")]
    public class ShowDialogueWindowUnit : Unit
    {
        [DoNotSerialize]
        public ControlInput input { get; private set; }

        [DoNotSerialize]
        [NullMeansSelf]
        public ValueInput dialogueSystem { get; private set; }

        [DoNotSerialize]
        public ControlOutput onStart { get; private set; }

        [DoNotSerialize]
        public ControlOutput onNext { get; private set; }

        public GraphReference graphReference;

        protected override void Definition()
        {
            input = ControlInput(nameof(input), Enter);
            dialogueSystem = ValueInput<DialogueSystem>(nameof(dialogueSystem), null)
                .NullMeansSelf();
            onNext = ControlOutput(nameof(onNext));
            onStart = ControlOutput(nameof(onStart));
            Succession(input, onNext);
            Succession(input, onStart);
        }

        private ControlOutput Enter(Flow flow)
        {
            DialogueSystem dialogueSystem = flow.GetValue<DialogueSystem>(this.dialogueSystem);
            graphReference = flow.stack.ToReference();

            dialogueSystem.ShowDialogueWindow(
                () =>
                {
                    Flow.New(graphReference).Invoke(this.onNext);
                }
            );
            return onStart;
        }
    }
}
