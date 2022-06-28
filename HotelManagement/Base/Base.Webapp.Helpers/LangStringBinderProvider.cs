using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Webapp.Helpers;

public class CustomLangStrBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(LangStr))
        {
            return new LangStrBinderProvider();
        }

        return null;
    }
}

public class LangStrBinderProvider : IModelBinder
{
    public LangStrBinderProvider()
    {
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (value == null)
        {
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(new LangStr(value));

        return Task.CompletedTask;
    }
}
