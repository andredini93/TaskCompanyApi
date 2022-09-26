using FluentValidation;
using FluentValidation.Results;
using Model;
using Model.Interfaces;

namespace Repository.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notifier(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notifier(error.ErrorMessage);
            }
        }

        protected void Notifier(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE>
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notifier(validator);

            return false;
        }
    }
}
