﻿/*
 * Copyright (c) 2017 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace SafeExamBrowser.Contracts.Logging
{
	public interface ILogger
	{
		/// <summary>
		/// Logs the given message with severity <b>INFO</b>.
		/// </summary>
		void Info(string message);

		/// <summary>
		/// Logs the given message with severity <b>WARNING</b>.
		/// </summary>
		void Warn(string message);

		/// <summary>
		/// Logs the given message with severity <b>ERROR</b>.
		/// </summary>
		void Error(string message);

		/// <summary>
		/// Logs the given message with severity <b>ERROR</b> and includes information about
		/// the specified exception (i.e. type, message and stacktrace).
		/// </summary>
		void Error(string message, Exception exception);

		/// <summary>
		/// Logs the given message as raw text.
		/// </summary>
		void Log(string message);

		/// <summary>
		/// Appends the given content to the log.
		/// </summary>
		void Log(ILogContent content);

		/// <summary>
		/// Suscribes an observer to the application log.
		/// </summary>
		void Subscribe(ILogObserver observer);

		/// <summary>
		/// Unsubscribes an observer from the application log.
		/// </summary>
		void Unsubscribe(ILogObserver observer);

		/// <summary>
		/// Returns a copy of the current log content.
		/// </summary>
		IList<ILogContent> GetLog();
	}
}