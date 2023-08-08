using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Playmeld
{
    [UnitTitle("SayDialogueMessage")]
    [UnitCategory("Playmeld")]
    public class SayDialogueMessageUnit : Unit
    {
        [DoNotSerialize]
        public ControlInput input { get; private set; }

        [DoNotSerialize]
        public ValueInput text { get; private set; }

        [DoNotSerialize]
        public ValueInput audioClip { get; private set; }

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
            text = ValueInput<string>(nameof(text), "");
            audioClip = ValueInput<AudioClip>(nameof(audioClip), null);
            dialogueSystem = ValueInput<DialogueSystem>(nameof(dialogueSystem), null)
                .NullMeansSelf();
            onNext = ControlOutput(nameof(onNext));
            onStart = ControlOutput(nameof(onStart));
            Requirement(text, input);
            Succession(input, onNext);
            Succession(input, onStart);
        }

        private ControlOutput Enter(Flow flow)
        {
            string text = flow.GetValue<string>(this.text);
            AudioClip audioClip = flow.GetValue<AudioClip>(this.audioClip);
            DialogueSystem dialogueSystem = flow.GetValue<DialogueSystem>(this.dialogueSystem);
            graphReference = flow.stack.ToReference();

            dialogueSystem.Say(
                text,
                audioClip,
                () =>
                {
                    Flow.New(graphReference).Invoke(this.onNext);
                }
            );
            return onStart;
        }
    }
}
