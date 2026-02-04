(function () {
  // STEP 2: Access DOM Objects
  const eventListEl = document.getElementById('eventList');
  const eventTitleEl = document.getElementById('eventTitle');
  const eventDescEl = document.getElementById('eventDesc');
  const countEl = document.getElementById('count');
  const registerBtn = document.getElementById('registerBtn');
  const unregisterBtn = document.getElementById('unregisterBtn');
  const addEventBtn = document.getElementById('addEventBtn');
  const addEventForm = document.getElementById('addEventForm');
  const newTitle = document.getElementById('newTitle');
  const newDesc = document.getElementById('newDesc');
  const saveNewBtn = document.getElementById('saveNewBtn');
  const cancelNewBtn = document.getElementById('cancelNewBtn');

  // STEP 3: Event Data (initial / persistent)
  const SAMPLE_EVENTS = [
    { id: 'e1', title: 'Morning Yoga', desc: 'An energizing 60-minute yoga session for all levels.', count: 5 },
    { id: 'e2', title: 'Book Club', desc: 'Monthly book discussion — bring your favorite reads.', count: 3 },
    { id: 'e3', title: 'Gardening Workshop', desc: 'Learn to pot plants and maintain your garden.', count: 0 }
  ];

  let events = loadEvents();
  let selectedId = null;

  // Utilities: storage
  function saveEvents() {
    try {
      localStorage.setItem('clubEvents', JSON.stringify(events));
    } catch (e) {
      console.warn('Could not save events to localStorage', e);
    }
  }

  function loadEvents() {
    try {
      const raw = localStorage.getItem('clubEvents');
      return raw ? JSON.parse(raw) : SAMPLE_EVENTS.slice();
    } catch (e) {
      console.warn('Failed to parse localStorage, falling back to defaults', e);
      return SAMPLE_EVENTS.slice();
    }
  }

  // STEP 4: Display Events Dynamically
  function renderEvents() {
    eventListEl.innerHTML = '';
    if (events.length === 0) {
      const li = document.createElement('li');//
      li.textContent = 'No events yet. Click "Add New Event" to create one.';
      li.style.color = '#666';
      eventListEl.appendChild(li);
      clearDetails();
      return;
    }

    events.forEach(ev => {
      const li = document.createElement('li');
      li.tabIndex = 0;
      li.dataset.id = ev.id;
      li.className = ev.id === selectedId ? 'selected' : '';

      const title = document.createElement('div');
      title.className = 'event-title';
      title.textContent = ev.title;

      const badge = document.createElement('span');
      badge.className = 'badge';
      badge.textContent = ev.count;
      badge.title = 'Participants';

      li.appendChild(title);
      li.appendChild(badge);

      // STEP 5: Handle Event Selection
      li.addEventListener('click', () => selectEvent(ev.id));
      li.addEventListener('keydown', (e) => { if (e.key === 'Enter' || e.key === ' ') { e.preventDefault(); selectEvent(ev.id); } });

      eventListEl.appendChild(li);
    });
  }

  function findEvent(id) {
    return events.find(e => e.id === id);
  }

  function selectEvent(id) {
    const ev = findEvent(id);
    if (!ev) return;
    selectedId = id;

    // update selection styles
    Array.from(eventListEl.children).forEach(li => li.classList.toggle('selected', li.dataset.id === id));

    // STEP 6: Show details
    eventTitleEl.textContent = ev.title;
    eventDescEl.textContent = ev.desc;
    countEl.textContent = ev.count;

    // enable/disable buttons
    registerBtn.disabled = false;
    unregisterBtn.disabled = ev.count === 0;
  }

  function clearDetails() {
    selectedId = null;
    eventTitleEl.textContent = 'Select an event';
    eventDescEl.textContent = 'Click an event from the list to view details.';
    countEl.textContent = '0';
    registerBtn.disabled = true;
    unregisterBtn.disabled = true;
  }

  // STEP 6: Register for Event
  registerBtn.addEventListener('click', () => {
    if (!selectedId) return alert('Please select an event first.');
    const ev = findEvent(selectedId);
    if (!ev) return alert('Selected event not found.');

    ev.count = Number(ev.count) + 1;
    countEl.textContent = ev.count;
    updateBadge(selectedId, ev.count);
    unregisterBtn.disabled = false;

    saveEvents();
  });

  // STEP 7: Unregister from Event
  unregisterBtn.addEventListener('click', () => {
    if (!selectedId) return alert('Please select an event first.');
    const ev = findEvent(selectedId);
    if (!ev) return alert('Selected event not found.');

    if (ev.count <= 0) return alert('No participants to remove.');

    // simple confirmation
    if (!confirm('Are you sure you want to unregister one participant from "' + ev.title + '"?')) return;

    ev.count = Math.max(0, Number(ev.count) - 1);
    countEl.textContent = ev.count;
    updateBadge(selectedId, ev.count);
    unregisterBtn.disabled = ev.count === 0;

    saveEvents();
  });

  function updateBadge(id, newCount) {
    const li = Array.from(eventListEl.children).find(li => li.dataset.id === id);
    if (!li) return;
    const badge = li.querySelector('.badge');
    if (badge) badge.textContent = newCount;
  }

  // STEP 8: Add New Event Dynamically
  addEventBtn.addEventListener('click', () => {
    addEventForm.style.display = addEventForm.style.display === 'none' || addEventForm.style.display === '' ? 'block' : 'none';
    if (addEventForm.style.display === 'block') {
      newTitle.focus();
    } else {
      resetAddForm();
    }
  });

  saveNewBtn.addEventListener('click', () => {
    const title = newTitle.value.trim();
    const desc = newDesc.value.trim();

    // STEP 9: Prevent Errors (Validation)
    if (!title) { alert('Title cannot be empty.'); newTitle.focus(); return; }
    if (title.length > 80) { alert('Title is too long (max 80 chars).'); newTitle.focus(); return; }

    const id = 'e' + Date.now();
    const newEvent = { id, title, desc: desc || 'No description provided.', count: 0 };
    events.push(newEvent);
    saveEvents();
    renderEvents();
    selectEvent(id);
    resetAddForm();
  });

  cancelNewBtn.addEventListener('click', () => {
    resetAddForm();
  });

  function resetAddForm() {
    addEventForm.style.display = 'none';
    newTitle.value = '';
    newDesc.value = '';
  }

  // keyboard: shortcuts
  document.addEventListener('keydown', (e) => {
    if (e.key === 'r' && (document.activeElement === document.body || document.activeElement === registerBtn)) {
      // 'r' to register
      if (!registerBtn.disabled) registerBtn.click();
    }
    if (e.key === 'u' && (document.activeElement === document.body || document.activeElement === unregisterBtn)) {
      if (!unregisterBtn.disabled) unregisterBtn.click();
    }
    if (e.key === 'n') {
      addEventBtn.click();
    }
  });

  // initialize
  renderEvents();
  if (events.length > 0) selectEvent(events[0].id);
})();