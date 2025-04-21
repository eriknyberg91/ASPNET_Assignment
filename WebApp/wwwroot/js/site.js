document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150
    // Open Modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)
            // Claude AI was used to help populate the edit project modal
            if (modal) {
                if (modalTarget === '#editProjectModal') {
                    const projectId = button.getAttribute('data-project-id')
                    const projectName = button.getAttribute('data-project-name')
                    const clientName = button.getAttribute('data-client-name')
                    const description = button.getAttribute('data-description')
                    const isCompleted = button.getAttribute('data-is-completed')
                    const startDate = button.getAttribute('data-start-date')
                    const endDate = button.getAttribute('data-end-date')
                    const budget = button.getAttribute('data-budget')

                    const idInput = modal.querySelector('input[name="Id"]')
                    const nameInput = modal.querySelector('input[name="ProjectName"]')
                    const clientInput = modal.querySelector('input[name="ClientName"]')
                    const descriptionInput = modal.querySelector('textarea[name="Description"]') || modal.querySelector('input[name="Description"]')
                    const isCompletedInput = modal.querySelector('input[name="IsCompleted"]')
                    const startDateInput = modal.querySelector('input[name="StartDate"]')
                    const endDateInput = modal.querySelector('input[name="EndDate"]')
                    const budgetInput = modal.querySelector('input[name="Budget"]') ||
                        modal.querySelector('input[name="budget"]')

                    if (idInput) idInput.value = projectId
                    if (nameInput) nameInput.value = projectName
                    if (clientInput) clientInput.value = clientName
                    if (descriptionInput) descriptionInput.value = description

                    if (isCompletedInput && isCompleted === 'True') {
                        isCompletedInput.checked = true
                    }

                    if (startDateInput) startDateInput.value = startDate
                    if (endDateInput && endDate) endDateInput.value = endDate
                    if (budgetInput && budget && budget !== "null" && budget !== "") {
                        const formattedBudget = budget.replace(',', '.');
                        budgetInput.value = formattedBudget;
                    } else {
                        budgetInput.value = '';
                    }

                }

                modal.style.display = 'flex'
            }
        })
    })

    // Close Modal
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')
            if (modal) {
                modal.style.display = 'none'

                modal.querySelectorAll('form').forEach(form => {
                    form.reset()

                    const imagePreview = form.querySelector('.image-preview')
                    if (imagePreview)
                        imagePreview.src = ''

                    const imagePreviewer = form.querySelector('image-previewer')
                    if (imagePreviewer)
                        imagePreviewer.classList.remove('selected')
                })
            }
        })
    })

    //TODO: Handle image-previewer
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0]
            if (file)
                processImage(file, imagePreview, previewer, previewSize)
                
        })
    })


    //handle submit forms
    const forms = document.querySelectorAll('form')
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            clearErrorMessages(form)

            const formData = new FormData(form)

            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData
                })

                if (res.ok) {
                    const modal = form.closest('.modal')
                    if (modal) {
                        modal.style.display = 'none'
                    }

                    //window.location.reload();
                        const data = await res.json();
                        if (data.redirectUrl) {
                            window.location.href = data.redirectUrl;
                        } else {
                            window.location.reload();
                        }
                    
                }

                else if (res.status === 400) {
                    const data = await res.json()

                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            let input = form.querySelector(`[name="${key}"]`)
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
                            if (span) {
                                span.innerText = data.errors[key].join('\n')
                                span.classList.add('field-validation-error')
                            }
                        })
                    }
                }
            }

            catch {
                console.log('Error on form submit.')
            }
        })
    })
})

function clearErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}

function addErrorMessage(key, errorMessage) {
    const input = form.querySelector(`[name="${key}"]`)
    if (input) {
        input.classList.add('input-validation-error')
    }

    const span = form.querySelector(`[data-valmsg-for="${key}"]`)
    if (span) {
        span.innerText = errorMessage
        span.classList.add('field-validation-error')
    }
}


async function loadImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader()

        reader.onerror = () => reject(new Error("Failed to load file."))

        reader.onload = (e) => {
            const img = new Image()
            img.onerror = () => reject(new Error("Failed to load image."))
            img.onload = () => resolve(img)
            img.src = e.target.result
        }

        reader.readAsDataURL(file)
    })
}

async function processImage(file, imagePreview, previewer, previewSize = 150) {
    try {
        const img = await loadImage(file)
        const canvas = document.createElement('canvas')
        canvas.width = previewSize
        canvas.height = previewSize

        const ctx = canvas.getContext('2d')
        ctx.drawImage(img, 0, 0, previewSize, previewSize)
        imagePreview.src = canvas.toDataURL('image/jpeg')
        previewer.classList.add('selected')
    }
    catch (error) {
        console.error('Failed on image processing.', error)
    }
}












