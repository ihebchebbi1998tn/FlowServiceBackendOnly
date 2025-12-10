(function () {
	// Check if base exists
	if (!window.Crm || !window.Crm.Service || !window.Crm.Service.ViewModels || !window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel) {
		console.error("DispatchDetailsChecklistsTabViewModel base not found - extension cannot load");
		return;
	}

	var basePrototype = window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel.prototype;

	// Add sortedChecklistItems as a function that returns sorted items
	// This works whether called before or after init
	basePrototype.sortedChecklistItems = function () {
		var viewModel = this;
		var allItems = viewModel.items ? viewModel.items() : [];
		
		if (!allItems || allItems.length === 0) {
			return [];
		}
		
		// Get current ServiceOrderTime ID from dispatch
		var currentServiceOrderTimeId = null;
		if (viewModel.dispatch && viewModel.dispatch() && viewModel.dispatch().CurrentServiceOrderTimeId) {
			currentServiceOrderTimeId = viewModel.dispatch().CurrentServiceOrderTimeId();
		}
		
		if (!currentServiceOrderTimeId) {
			return allItems;
		}
		
		// Sort: current job's checklists first, then maintain original order
		return allItems.slice().sort(function (a, b) {
			var aIsCurrentJob = a.ServiceOrderTimeKey && a.ServiceOrderTimeKey() === currentServiceOrderTimeId;
			var bIsCurrentJob = b.ServiceOrderTimeKey && b.ServiceOrderTimeKey() === currentServiceOrderTimeId;
			
			if (aIsCurrentJob && !bIsCurrentJob) return -1;
			if (!aIsCurrentJob && bIsCurrentJob) return 1;
			return 0;
		});
	};

	console.log("DispatchDetailsChecklistsTabViewModelExtension loaded successfully");
})();
