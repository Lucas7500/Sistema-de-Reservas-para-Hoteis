function cadastraUsuario (body) {
    const options = {
      method: 'POST',
      body: Object.keys(body)
        .map(k => `${encodeURIComponent(k)}=${encodeURIComponent(body[k])}`)
        .join('&')
    }

    return fetch('http://exemplo.com/usuario', options)
      .then(T => T.json())
  }

  cadastraUsuario({ nome: 'Bruno', sobrenome: 'Carvalho de Araujo' })
    .then(() => console.log('cadastrado'))
    .catch(() => console.log('falha ao cadastrar'))