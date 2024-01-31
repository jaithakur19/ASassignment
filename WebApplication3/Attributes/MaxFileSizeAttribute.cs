using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class MaxFileSizeAttribute : ValidationAttribute
{
	private readonly long _maxFileSize;

	public MaxFileSizeAttribute(long maxFileSize)
	{
		_maxFileSize = maxFileSize;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var file = value as IFormFile;

		if (file != null)
		{
			if (file.Length > _maxFileSize)
			{
				return new ValidationResult(GetErrorMessage());
			}
		}

		return ValidationResult.Success;
	}

	public string GetErrorMessage()
	{
		return $"File size cannot exceed {_maxFileSize / 1024 / 1024} MB.";
	}
}
