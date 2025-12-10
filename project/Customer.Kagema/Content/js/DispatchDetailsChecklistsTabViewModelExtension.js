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
		
		// First call base initItems (which sorts by title and itemGroup)
		var result = baseInitItems.apply(viewModel, arguments);
		
		// Now get the current ServiceOrderTime ID from the dispatch
		var currentServiceOrderTimeId = null;
		if (viewModel.dispatch && viewModel.dispatch() && viewModel.dispatch().CurrentServiceOrderTimeId) {
			currentServiceOrderTimeId = viewModel.dispatch().CurrentServiceOrderTimeId();
		}

		// Re-sort AFTER base to put current job's checklists first
		if (currentServiceOrderTimeId && viewModel.items && viewModel.items()) {
			var sortedItems = viewModel.items().slice().sort(function (a, b) {
				var aIsCurrentJob = a.ServiceOrderTimeKey && a.ServiceOrderTimeKey() === currentServiceOrderTimeId;
				var bIsCurrentJob = b.ServiceOrderTimeKey && b.ServiceOrderTimeKey() === currentServiceOrderTimeId;
				
				if (aIsCurrentJob && !bIsCurrentJob) return -1;
				if (!aIsCurrentJob && bIsCurrentJob) return 1;
				return 0; // Keep existing order within groups
			});
			viewModel.items(sortedItems);
		}

		return result;
	};

	console.log("DispatchDetailsChecklistsTabViewModelExtension loaded successfully");
})();
