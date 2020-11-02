import React, { FC, CSSProperties } from 'react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import { AppMenu } from './AppMenu'
import { Layout } from 'antd'
import { routes } from './routes'
import Common from './Common'

const { Header, Footer, Content } = Layout

const styles: {
  layout: CSSProperties
  header: CSSProperties
  content: CSSProperties
  footer: CSSProperties
} = {
  layout: {
    height: '100%',
  },
  header: {
    backgroundColor: 'rgb(9, 109, 217)',
  },
  content: {
    marginTop: 3,
    marginLeft: 50,
    marginRight: 50,
  },
  footer: {
    textAlign: 'center',
  },
}

//TODO: use effect store / load

export const App: FC = () => (
  <Router>
    <Common />
    <Layout style={styles.layout}>
      <Header style={styles.header}>
        <AppMenu routes={routes} />
      </Header>
      <Content style={styles.content}>
        <Switch>
          {routes.map((r) => (
            <Route key={r.path} {...r} />
          ))}
        </Switch>
      </Content>
      <Footer style={styles.footer}>&copy; 2020 Henryroach</Footer>
    </Layout>
  </Router>
)
