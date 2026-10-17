(() => {
  // Show / hide password
  document.querySelectorAll('[data-toggle-password]').forEach(btn => {
    btn.addEventListener('click', () => {
      const input = btn.closest('.field').querySelector('input');
      const show = input.type === 'password';
      input.type = show ? 'text' : 'password';
      btn.classList.toggle('is-on', show);
      btn.setAttribute('aria-pressed', String(show));
      btn.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
    });
  });

  // Loading state once the form is actually submitting (skips if validation blocked it)
  document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', e => {
      setTimeout(() => {
        if (e.defaultPrevented) return;
        const btn = form.querySelector('.btn-auth');
        if (btn) { btn.classList.add('is-loading'); btn.disabled = true; }
      }, 0);
    });
  });
  window.addEventListener('pageshow', e => {
    if (!e.persisted) return;
    document.querySelectorAll('.btn-auth').forEach(b => { b.classList.remove('is-loading'); b.disabled = false; });
  });

  // Password strength meter (Register only)
  const meter = document.querySelector('[data-strength]');
  const pwd = document.querySelector('[data-strength-input]');
  if (meter && pwd) {
    const bars = [...meter.children];
    pwd.addEventListener('input', () => {
      const v = pwd.value;
      let score = 0;
      if (v.length >= 8) score++;
      if (/[a-z]/.test(v) && /[A-Z]/.test(v)) score++;
      if (/\d/.test(v)) score++;
      if (/[^A-Za-z0-9]/.test(v)) score++;
      if (!v) score = 0;
      meter.dataset.level = score;
      bars.forEach((b, i) => b.classList.toggle('on', i < score));
    });
  }
})();
