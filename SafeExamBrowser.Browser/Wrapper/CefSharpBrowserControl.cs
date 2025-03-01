﻿/*
 * Copyright (c) 2022 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using CefSharp;
using CefSharp.WinForms;
using SafeExamBrowser.Browser.Handlers;
using SafeExamBrowser.Browser.Wrapper.Events;
using SafeExamBrowser.Browser.Wrapper.Handlers;

namespace SafeExamBrowser.Browser.Wrapper
{
	internal class CefSharpBrowserControl : ChromiumWebBrowser, ICefSharpControl
	{
		public event AuthCredentialsEventHandler AuthCredentialsRequired;
		public event BeforeBrowseEventHandler BeforeBrowse;
		public event BeforeDownloadEventHandler BeforeDownload;
		public event DownloadUpdatedEventHandler DownloadUpdated;
		public event FaviconUrlChangedEventHandler FaviconUrlChanged;
		public event FileDialogRequestedEventHandler FileDialogRequested;
		public event KeyEventHandler KeyEvent;
		public event LoadingProgressChangedEventHandler LoadingProgressChanged;
		public event OpenUrlFromTabEventHandler OpenUrlFromTab;
		public event PreKeyEventHandler PreKeyEvent;
		public event ResourceRequestEventHandler ResourceRequestHandlerRequired;

		public CefSharpBrowserControl(ILifeSpanHandler lifeSpanHandler, string url) : base(url)
		{
			DialogHandler = new DialogHandlerSwitch();
			DisplayHandler = new DisplayHandlerSwitch();
			DownloadHandler = new DownloadHandlerSwitch();
			KeyboardHandler = new KeyboardHandlerSwitch();
			LifeSpanHandler = lifeSpanHandler;
			MenuHandler = new ContextMenuHandler();
			RequestHandler = new RequestHandlerSwitch();
		}

		void ICefSharpControl.Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		public void GetAuthCredentials(IWebBrowser webBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback, GenericEventArgs args)
		{
			AuthCredentialsRequired?.Invoke(webBrowser, browser, originUrl, isProxy, host, port, realm, scheme, callback, args);
		}

		public void GetResourceRequestHandler(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling, ResourceRequestEventArgs args)
		{
			ResourceRequestHandlerRequired?.Invoke(webBrowser, browser, frame, request, isNavigation, isDownload, requestInitiator, ref disableDefaultHandling, args);
		}

		public void OnBeforeBrowse(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect, GenericEventArgs args)
		{
			BeforeBrowse?.Invoke(webBrowser, browser, frame, request, userGesture, isRedirect, args);
		}

		public void OnBeforeDownload(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
		{
			BeforeDownload?.Invoke(webBrowser, browser, downloadItem, callback);
		}

		public void OnDownloadUpdated(IWebBrowser webBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
		{
			DownloadUpdated?.Invoke(webBrowser, browser, downloadItem, callback);
		}

		public void OnFaviconUrlChange(IWebBrowser webBrowser, IBrowser browser, IList<string> urls)
		{
			FaviconUrlChanged?.Invoke(webBrowser, browser, urls);
		}

		public void OnFileDialog(IWebBrowser webBrowser, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
		{
			FileDialogRequested?.Invoke(webBrowser, browser, mode, flags, title, defaultFilePath, acceptFilters, selectedAcceptFilter, callback);
		}

		public void OnKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
		{
			KeyEvent?.Invoke(webBrowser, browser, type, windowsKeyCode, nativeKeyCode, modifiers, isSystemKey);
		}

		public void OnLoadingProgressChange(IWebBrowser webBrowser, IBrowser browser, double progress)
		{
			LoadingProgressChanged?.Invoke(webBrowser, browser, progress);
		}

		public void OnOpenUrlFromTab(IWebBrowser webBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture, GenericEventArgs args)
		{
			OpenUrlFromTab?.Invoke(webBrowser, browser, frame, targetUrl, targetDisposition, userGesture, args);
		}

		public void OnPreKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut, GenericEventArgs args)
		{
			PreKeyEvent?.Invoke(webBrowser, browser, type, windowsKeyCode, nativeKeyCode, modifiers, isSystemKey, ref isKeyboardShortcut, args);
		}
	}
}
