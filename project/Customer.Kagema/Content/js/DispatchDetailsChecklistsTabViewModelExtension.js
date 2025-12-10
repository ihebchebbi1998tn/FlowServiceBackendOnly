(function () {
	// Check if base exists
	if (!window.Crm || !window.Crm.Service || !window.Crm.Service.ViewModels || !window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel) {
		console.error("DispatchDetailsChecklistsTabViewModel base not found - extension cannot load");
		return;
	}

	var basePrototype = window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel.prototype;
	var baseInitItems = basePrototype.initItems;

	// Override initItems to prioritize checklists for the currently selected job at the top
	basePrototype.initItems = function (items) {
		var viewModel = this;
		var currentServiceOrderTimeId = null;

		// Get the current ServiceOrderTime ID from the dispatch
		if (viewModel.dispatch && viewModel.dispatch() && viewModel.dispatch().CurrentServiceOrderTimeId) {
			currentServiceOrderTimeId = viewModel.dispatch().CurrentServiceOrderTimeId();
		}

		// Call the base initItems first (this does the standard sorting on items array)
		var result = baseInitItems.apply(viewModel, arguments);

		// Then re-sort the items array to put current job's checklists at the top
		if (currentServiceOrderTimeId) {
			// Sort the items array directly (same pattern as base)
			items.sort(function (a, b) {
				var aIsCurrentJob = a.ServiceOrderTimeKey && a.ServiceOrderTimeKey() === currentServiceOrderTimeId;
				var bIsCurrentJob = b.ServiceOrderTimeKey && b.ServiceOrderTimeKey() === currentServiceOrderTimeId;
				
				if (aIsCurrentJob && !bIsCurrentJob) return -1;
				if (!aIsCurrentJob && bIsCurrentJob) return 1;
				return 0; // Keep existing order within groups
			});
			
			// Re-apply to viewModel.items if it exists
			if (viewModel.items && typeof viewModel.items === 'function') {
				viewModel.items(items);
			}
		}

		return result;
	};

	console.log("DispatchDetailsChecklistsTabViewModelExtension loaded successfully");
})();
