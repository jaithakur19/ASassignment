﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class AllowedExtensionsAttribute : ValidationAttribute
{
	private readonly string[] _extensions;

	public AllowedExtensionsAttribute(string[] extensions)
	{
		_extensions = extensions;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var file = value as IFormFile;

		if (file != null)
		{
			var extension = System.IO.Path.GetExtension(file.FileName);

			if (!_extensions.Contains(extension.ToLower()))
			{
				return new ValidationResult(GetErrorMessage());
			}
		}

		return ValidationResult.Success;
	}

	public string GetErrorMessage()
	{
		return "Only specific file types are allowed.";
	}
}
