function showError(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("err")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }else{
        throw new Error("É necessário a criação de uma div com o id 'notificacao'")
    }
  }

function showSuccess(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("success")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }else{
        throw new Error("É necessário a criação de uma div com o id 'notificacao'")
    }
  }

  export {showSuccess, showError}