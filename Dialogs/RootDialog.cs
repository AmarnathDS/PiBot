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
            if (activity.Text.ToLowerInvariant().Contains("order"))
            {
                // ask the user to confirm if they want to reset the counter
                //var options = new PromptOptions<string>(prompt: "Are you sure you want to reset the count?",
                //    retry: "Didn't get that!", speak: "Do you want me to reset the counter?",
                //    retrySpeak: "You can say yes or no!",
                //    options: PromptDialog.PromptConfirm.Options,
                //    promptStyler: new PromptStyler());

                //PromptDialog.Confirm(context, AfterResetAsync, options);
                await context.SayAsync($"Please say the order number", $"Please say the order number", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);

            }
            else if (activity.Text.ToLowerInvariant().StartsWith("m"))
            {
                // calculate something for us to return
                //int length = activity.Text.Length;

                // increment the counter
                //this.count++;

                //Activity reply = activity.CreateReply($"{count}: You sent {activity.Text} which was {length} characters");
                //reply.Speak = $"{count}: You said {activity.Text}";
                //reply.InputHint = InputHints.ExpectingInput;
                //await context.PostAsync(reply);



                // say reply to the user
                await context.SayAsync($"order {activity.Text} is deliverd to plant", $"order {activity.Text} is deliverd to plant", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text.ToLowerInvariant().StartsWith("a"))
            {
                // calculate something for us to return
                //int length = activity.Text.Length;

                // increment the counter
                //this.count++;

                //Activity reply = activity.CreateReply($"{count}: You sent {activity.Text} which was {length} characters");
                //reply.Speak = $"{count}: You said {activity.Text}";
                //reply.InputHint = InputHints.ExpectingInput;
                //await context.PostAsync(reply);



                // say reply to the user
                await context.SayAsync($"order {activity.Text} is in transit", $"order {activity.Text} is in transit", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text.ToLowerInvariant().Contains("thanks"))
            {
                // calculate something for us to return
                //int length = activity.Text.Length;

                // increment the counter
                //this.count++;

                //Activity reply = activity.CreateReply($"{count}: You sent {activity.Text} which was {length} characters");
                //reply.Speak = $"{count}: You said {activity.Text}";
                //reply.InputHint = InputHints.ExpectingInput;
                //await context.PostAsync(reply);



                // say reply to the user
                await
                    context.SayAsync($"Happy to help", $"Happy to help",
                        new MessageOptions() {InputHint = InputHints.ExpectingInput});
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text.ToLowerInvariant().Contains("part"))
            {
                await context.SayAsync($"Please say the part number", $"Please say the part number", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text.ToLowerInvariant().Equals("2215656"))
            {
                await context.SayAsync($"Part Qty for part {activity.Text} is 5", $"Part Qty for part {activity.Text} is 5", new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await
                       context.SayAsync($"Sorry did not get that. Please say again", $"Sorry did not get that. Please say again",
                           new MessageOptions() { InputHint = InputHints.ExpectingInput });
                context.Wait(MessageReceivedAsync);

            }

        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            // check if user wants to reset the counter or not
            if (confirm)
            {
                this.count = 1;
                await context.SayAsync("Reset count.", "I reset the counter for you!");
            }
            else
            {
                await context.SayAsync("Did not reset count.", $"Counter is not reset. Current value: {this.count}");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}