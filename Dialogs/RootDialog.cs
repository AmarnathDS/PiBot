using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace PiBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private int count;

        public Task StartAsync(IDialogContext context)
        {
            this.count = 0;
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            activity.Text = activity.Text ?? string.Empty;

            // check if the user said reset
            if (activity.Text.ToLowerInvariant().Contains("order") || activity.Text.ToLowerInvariant().Contains("details"))
            {
                // ask the user to confirm if they want to reset the counter
                //var options = new PromptOptions<string>(prompt: "Are you sure you want to reset the count?",
                //    retry: "Didn't get that!", speak: "Do you want me to reset the counter?",
                //    retrySpeak: "You can say yes or no!",
                //    options: PromptDialog.PromptConfirm.Options,
                //    promptStyler: new PromptStyler());

                //PromptDialog.Confirm(context, AfterResetAsync, options);
                await context.SayAsync($"Please say the order number", $"Please say the order number", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(AfterResetAsync);

            }
            
            else if (activity.Text.ToLowerInvariant().Contains("thank"))
            {
                // calculate something for us to return
                //int length = activity.Text.Length;

                // increment the counter
                //this.count++;

                //Activity reply = activity.CreateReply($"{count}: You sent {activity.Text} which was {length} characters");
                //reply.Speak = $"{count}: You said {activity.Text}";
                //reply.InputHint = InputHints.ExpectingInput;
                //await context.PostAsync(reply);

                var rnd = new Random();
                var say = rnd.Next(1, 3);

                switch (say)
                {
                    case 1:
                        await
                        context.SayAsync($"Happy to help. Hope i have answered your queries", $"Happy to help. Hope i have answered your queries",
                   new MessageOptions() { InputHint = InputHints.ExpectingInput });
                        break;
                    case 2:
                        await
                        context.SayAsync($"No Problem. Happy to help", $"No Problem. Happy to help",
                   new MessageOptions() { InputHint = InputHints.ExpectingInput });
                        break;
                    case 3:
                        await
                        context.SayAsync($"Any time. Happy to help", $"Any time. Happy to help",
                       new MessageOptions() { InputHint = InputHints.ExpectingInput });
                        break;
                    default:
                        await
                    context.SayAsync($"No Problem. Any time", $"No Problem. Any time",
                   new MessageOptions() { InputHint = InputHints.ExpectingInput });
                        break;

                }

                // say reply to the user
               
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text.ToLowerInvariant().Contains("part") || activity.Text.ToLowerInvariant().Contains("number"))
            {
                await context.SayAsync($"Please say the part number", $"Please say the part number", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(AfterResetPartAsync);
            }
            else
            {
                await
                       context.SayAsync($"Sorry did not get that. Please say again", $"Sorry did not get that. Please say again",
                           new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);

            }

        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var confirm = await argument;

            var rnd = new Random();
            var say = rnd.Next(1, 3);

            switch (say)
            {
                case 1:
                    await
                    context.SayAsync($"order {confirm.Text} is deliverd to plant", $"order {confirm.Text} is deliverd to plant", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
                case 2:
                    await context.SayAsync($"order {confirm.Text} is in transit. Expected arrival is {DateTime.Today.Date.AddDays(2).ToShortDateString()}", $"order {confirm.Text} is in transit. Expected arrival is {DateTime.Today.Date.AddDays(2).ToShortDateString()}", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
                default:
                    await
                context.SayAsync($"order {confirm.Text} is deliverd to plant", $"order {confirm.Text} is deliverd to plant", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
            }
                context.Wait(MessageReceivedAsync); 
               
        }

        public async Task AfterResetPartAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var confirm = await argument;

            var rnd = new Random();
            var say = rnd.Next(1, 3);

            switch (say)
            {
                case 1:
                    await
                    context.SayAsync($"Part Qty for part {confirm.Text} is 5. The rack location for the part {confirm.Text} is WH010", $"Part Qty for part {confirm.Text} is 5. The rack location for the part {confirm.Text} is WH010", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
                case 2:
                    await context.SayAsync($"Part Qty for part {confirm.Text} is 8. The rack location for the part {confirm.Text} is RBH113", $"Part Qty for part {confirm.Text} is 8. The rack location for the part {confirm.Text} is RBH113", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
                default:
                    await
                context.SayAsync($"Part Qty for part {confirm.Text} is 5. The rack location for the part {confirm.Text} is WH010", $"Part Qty for part {confirm.Text} is 5. The rack location for the part {confirm.Text} is WH010", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                    break;
            }
            context.Wait(MessageReceivedAsync);

        }
    }
}